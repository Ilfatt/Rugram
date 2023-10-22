using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Auth.Data;
using Auth.Models;
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

    public async Task<bool> IsValidRefreshToken(string inputToken, Guid userId)
    {
        var tokenFromCache = await _cache.GetStringAsync(userId.ToString());

        if (inputToken == tokenFromCache) return true;

        var tokenFromDb = await _dbContext.RefreshTokens.AsNoTracking()
            .FirstOrDefaultAsync(token => token.UserId == userId &&
                                          token.ValidTo < DateTime.UtcNow);

        if (tokenFromDb == null) return false;

        var slidingExpiration = int.Parse(_configuration.GetValue<string>(
            "AuthOptions:SlidingExpirationInCacheForRefreshTokenInMinutes")!);

        if (tokenFromDb.ValidTo + TimeSpan.FromMinutes(slidingExpiration) > DateTime.UtcNow) return true;

        await _cache.SetStringAsync(
            userId.ToString(),
            tokenFromDb.Token,
            new DistributedCacheEntryOptions()
            {
                SlidingExpiration = TimeSpan.FromMinutes(slidingExpiration)
            });

        return true;
    }

    public async Task<RefreshToken> CreateRefreshToken(Guid userId)
    {
        var token = HashSha256(GenerateSecureToken());
        var slidingExpiration = int.Parse(_configuration.GetValue<string>(
            "AuthOptions:SlidingExpirationInCacheForRefreshTokenInMinutes")!);

        await _cache.SetStringAsync(
            userId.ToString(),
            token,
            new DistributedCacheEntryOptions()
            {
                SlidingExpiration = TimeSpan.FromMinutes(slidingExpiration)
            });

        var refreshTokenLifetimeInHours = int.Parse(
            _configuration.GetValue<string>("AuthOptions:RefreshTokenLifetimeInHours")!);

        return new RefreshToken
        {
            Token = token,
            ValidTo = DateTime.UtcNow + TimeSpan.FromHours(refreshTokenLifetimeInHours),
            UserId = userId
        };
    }

    public string GenerateAccessTokenForUserAsync(Guid userId,Role role)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var jwtSecurityKey = Encoding.ASCII.GetBytes(
            _configuration.GetValue<string>("AuthOptions:JwtSecretKey")!);
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, userId.ToString()),
            new(ClaimTypes.Role,((int)role).ToString())
        };

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Audience = _configuration.GetValue<string>("AuthOptions:Audience"),
            Issuer = _configuration.GetValue<string>("AuthOptions:Issuer"),
            Subject = new ClaimsIdentity(claims.ToArray()),
            Expires = DateTime.UtcNow.AddMinutes(int.Parse(
                _configuration.GetValue<string>("AuthOptions:AccessTokenLifetimeInMinutes")!)),
            SigningCredentials =
                new SigningCredentials(new SymmetricSecurityKey(jwtSecurityKey),
                    SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    private static string GenerateSecureToken()
    {
        var randomNumber = new byte[32];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);

        return HashSha256(Convert.ToBase64String(randomNumber));
    }

    private static string HashSha256(string inputString)
    {
        var inputBytes = Encoding.UTF32.GetBytes(inputString);
        var hashedBytes = SHA256.HashData(inputBytes);

        return Convert.ToBase64String(hashedBytes);
    }
}