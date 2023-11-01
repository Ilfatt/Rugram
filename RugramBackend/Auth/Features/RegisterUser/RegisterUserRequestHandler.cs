using Auth.Data;
using Auth.Data.Models;
using Infrastructure.MediatR.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using static Auth.Features.UserAuthHelper;

namespace Auth.Features.RegisterUser;

public class RegisterUserRequestHandler : IGrpcRequestHandler<RegisterUserRequest, RegisterUserResponse>
{
    private readonly AppDbContext _dbContext;
    private readonly IConfiguration _configuration;
    private readonly IDistributedCache _cache;

    public RegisterUserRequestHandler(
        AppDbContext dbContext,
        IConfiguration configuration,
        IDistributedCache cache)
    {
        _dbContext = dbContext;
        _configuration = configuration;
        _cache = cache;
    }

    public async Task<GrpcResult<RegisterUserResponse>> Handle(
        RegisterUserRequest request,
        CancellationToken cancellationToken)
    {
        var userWithThisEmailExist = await _dbContext.Users.AsNoTracking()
            .AnyAsync(user => user.Email == request.Email, cancellationToken: cancellationToken);

        if (userWithThisEmailExist)
        {
            return StatusCodes.Status409Conflict;
        }

        var hashedToken = request.MailConfirmationToken.HashSha256();
        var mailConfirmationToken = await _dbContext.MailConfirmationTokens.AsNoTracking()
            .FirstOrDefaultAsync(token => token.Email == request.Email &&
                                          token.Value == hashedToken, cancellationToken);

        if (mailConfirmationToken == null)
        {
            return StatusCodes.Status404NotFound;
        }

        if (mailConfirmationToken.ValidTo < DateTime.UtcNow)
        {
            return StatusCodes.Status403Forbidden;
        }

        var user = new User
        {
            Email = request.Email,
            Password = request.Password.HashSha256(),
            RefreshTokens = new List<RefreshToken>(),
            Role = Role.User,
        };

        var result = CreateRefreshToken(_configuration, user.Id);

        await PutInCacheRefreshToken(
            _configuration,
            _cache,
            result.RefreshToken.Value,
            result.RefreshToken.ValidTo,
            user.Id,
            cancellationToken);

        user.RefreshTokens.Add(result.RefreshToken);

        _dbContext.Users.Add(user);
        _dbContext.RefreshTokens.Add(result.RefreshToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        var jwtToken = GenerateJwtToken(_configuration, user.Id, user.Role);

        return new RegisterUserResponse(
            result.UnhashedTokenValue,
            jwtToken);
    }
}