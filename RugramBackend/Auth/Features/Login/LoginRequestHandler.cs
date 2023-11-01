using Auth.Data;
using Infrastructure.MediatR.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using static Auth.Features.UserAuthHelper;

namespace Auth.Features.Login;

public class LoginRequestHandler : IGrpcRequestHandler<LoginRequest, LoginResponse>
{
    private readonly AppDbContext _dbContext;
    private readonly IConfiguration _configuration;
    private readonly IDistributedCache _cache;

    public LoginRequestHandler(
        AppDbContext dbContext,
        IConfiguration configuration,
        IDistributedCache cache)
    {
        _configuration = configuration;
        _cache = cache;
        _dbContext = dbContext;
    }

    public async Task<GrpcResult<LoginResponse>> Handle(LoginRequest request, CancellationToken cancellationToken)
    {
        var user = await _dbContext.Users.AsNoTracking()
            .Select(user => new { user.Id, user.Email, user.Password, user.Role })
            .FirstOrDefaultAsync(user => user.Email == request.Email, cancellationToken);

        if (user == null) return 404;
        if (user.Password != request.Password.HashSha256()) return 403;

        var jwtToken = GenerateJwtToken(_configuration, user.Id, user.Role);
        var result = CreateRefreshToken(_configuration, user.Id);

        await PutInCacheRefreshToken(
            _configuration,
            _cache,
            result.RefreshToken.Value,
            result.RefreshToken.ValidTo,
            user.Id,
            cancellationToken);

        _dbContext.RefreshTokens.Add(result.RefreshToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return new LoginResponse(jwtToken, result.UnhashedTokenValue);
    }
}