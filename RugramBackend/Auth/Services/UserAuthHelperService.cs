using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Auth.Data;
using Auth.Data.Models;
using Auth.Services.Dto;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.IdentityModel.Tokens;

namespace Auth.Services;

public class UserAuthHelperService
{
    private readonly IDistributedCache _cache;
    private readonly AppDbContext _dbContext;
    private readonly IConfiguration _configuration;

    public UserAuthHelperService(
        IDistributedCache cache,
        AppDbContext dbContext,
        IConfiguration configuration)
    {
        _cache = cache;
        _dbContext = dbContext;
        _configuration = configuration;
    }

    public void SendMessage(string subject, string body, string sendTo)
    {
        var from = new MailAddress(_configuration["EmailConfig:Sender"]!,
            _configuration["EmailConfig:SenderName"]!);
        var to = new MailAddress(sendTo);
        var message = new MailMessage(from, to)
        {
            Subject = subject,
            Body = body,
            IsBodyHtml = true
        };

        var smtp = new SmtpClient(_configuration["SmtpSettings:SmtpAddress"],
            int.Parse(_configuration["SmtpSettings:Port"]!))
        {
            Credentials = new NetworkCredential(_configuration["EmailConfig:Sender"]!,
                _configuration["EmailConfig:SenderPassword"]!),
            EnableSsl = true,
            UseDefaultCredentials = false,
        };

        smtp.SendMailAsync(message);
    }

    public async Task<bool> IsValidRefreshToken(string inputToken, Guid userId)
    {
        var hashedInputToken = HashSha256(inputToken);
        var tokenFromCache = await _cache.GetStringAsync(userId.ToString());

        if (hashedInputToken == tokenFromCache) return true;

        var tokenFromDb = await _dbContext.RefreshTokens.AsNoTracking()
            .FirstOrDefaultAsync(token => token.UserId == userId &&
                                          token.Value == hashedInputToken &&
                                          token.ValidTo < DateTime.UtcNow);

        if (tokenFromDb == null) return false;

        var slidingExpiration = int.Parse(_configuration.GetValue<string>(
            "Cache:SlidingExpirationForRefreshTokenInMinutes")!);

        if (tokenFromDb.ValidTo + TimeSpan.FromMinutes(slidingExpiration) > DateTime.UtcNow) return true;

        await _cache.SetStringAsync(
            userId.ToString(),
            tokenFromDb.Value,
            new DistributedCacheEntryOptions
            {
                SlidingExpiration = TimeSpan.FromMinutes(slidingExpiration)
            });

        return true;
    }

    public async Task<CreateRefreshTokenResponse> CreateRefreshToken(Guid userId)
    {
        var token = HashSha256(GenerateSecureToken());
        var slidingExpiration = int.Parse(
            _configuration["Cache:SlidingExpirationForRefreshTokenInMinutes"]!);

        await _cache.SetStringAsync(
            userId.ToString(),
            HashSha256(token),
            new DistributedCacheEntryOptions()
            {
                SlidingExpiration = TimeSpan.FromMinutes(slidingExpiration)
            });

        var refreshTokenLifetimeInHours = int.Parse(
            _configuration["AuthOptions:RefreshTokenLifetimeInHours"]!);

        return new CreateRefreshTokenResponse(
            token,
            new RefreshToken()
            {
                Value = HashSha256(token),
                ValidTo = DateTime.UtcNow + TimeSpan.FromHours(refreshTokenLifetimeInHours),
                UserId = userId
            });
    }

    public string GenerateJwtToken(Guid userId, Role role)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var jwtSecurityKey = Encoding.ASCII.GetBytes(
            _configuration["AuthOptions:JwtSecretKey"]!);
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, userId.ToString()),
            new(ClaimTypes.Role, ((int)role).ToString())
        };

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Audience = _configuration["AuthOptions:Audience"],
            Issuer = _configuration["AuthOptions:Issuer"],
            Subject = new ClaimsIdentity(claims.ToArray()),
            Expires = DateTime.UtcNow.AddMinutes(int.Parse(
                _configuration["AuthOptions:AccessTokenLifetimeInMinutes"]!)),
            SigningCredentials =
                new SigningCredentials(new SymmetricSecurityKey(jwtSecurityKey),
                    SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    public static string GenerateSecureToken()
    {
        var randomNumber = new byte[32];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);

        return HashSha256(Convert.ToBase64String(randomNumber));
    }

    public static string HashSha256(string inputString)
    {
        var inputBytes = Encoding.UTF32.GetBytes(inputString);
        var hashedBytes = SHA256.HashData(inputBytes);

        return Convert.ToBase64String(hashedBytes);
    }
}