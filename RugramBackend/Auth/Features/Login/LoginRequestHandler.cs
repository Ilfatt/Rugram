using Auth.Data;
using Auth.Services;
using Infrastructure.MediatR.Contracts;
using Microsoft.EntityFrameworkCore;
using static Auth.Services.UserAuthHelperService;

namespace Auth.Features.Login;

public class LoginRequestHandler : IGrpcRequestHandler<LoginRequest, LoginResponse>
{
    private readonly AppDbContext _dbContext;
    private readonly UserAuthHelperService _userAuthHelperService;

    public LoginRequestHandler(
        AppDbContext dbContext, 
        UserAuthHelperService userAuthHelperService)
    {
        _dbContext = dbContext;
        _userAuthHelperService = userAuthHelperService;
    }

    public async Task<GrpcResult<LoginResponse>> Handle(LoginRequest request, CancellationToken cancellationToken)
    {
        var user = await _dbContext.Users.AsNoTracking()
            .Select(user => new { user.Id, user.Email, user.Password, user.Role })
            .FirstOrDefaultAsync(user => user.Email == request.Email, cancellationToken);

        if (user == null) return 404;
        if (user.Password != HashSha256(request.Password)) return 403;

        var jwtToken = _userAuthHelperService.GenerateJwtToken(user.Id, user.Role);
        var result = await _userAuthHelperService.CreateRefreshToken(user.Id);

        _dbContext.RefreshTokens.Add(result.RefreshToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return new LoginResponse(jwtToken,result.UnhashedTokenValue);
    }
}