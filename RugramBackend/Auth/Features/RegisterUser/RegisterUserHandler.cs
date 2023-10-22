using Auth.Data;
using Auth.Models;
using Auth.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Auth.Features.RegisterUser;

public class RegisterUserHandler : IRequestHandler<RegisterUserRequest,RegisterUserResponse>
{
    private readonly AppDbContext _dbContext;
    private readonly UserAuthHelperService _userAuthHelperService;

    public RegisterUserHandler(
        AppDbContext dbContext,
        UserAuthHelperService userAuthHelperService)
    {
        _userAuthHelperService = userAuthHelperService;
        _dbContext = dbContext;
    }
    public async Task<RegisterUserResponse> Handle(RegisterUserRequest request, CancellationToken cancellationToken)
    {
        var countUserWithSameEmail = await _dbContext.Users.AsNoTracking()
            .CountAsync(user => user.Email == request.Email, cancellationToken: cancellationToken);

        if (countUserWithSameEmail > 0)
        {
            return new RegisterUserResponse("", "", StatusCodes.Status409Conflict);
        }

        var user = new User
        {
            Email = request.Email,
            Password = request.Password,
            RefreshTokens = new List<RefreshToken>(),
            Role = Role.User,
        };

        var refreshToken = await _userAuthHelperService.CreateRefreshToken(user.Id);
        user.RefreshTokens.Add(refreshToken);

        _dbContext.Users.Add(user);
        _dbContext.RefreshTokens.Add(refreshToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        
        var jwtToken = _userAuthHelperService.GenerateAccessTokenForUserAsync(user.Id, user.Role);
        
        return new RegisterUserResponse(jwtToken,refreshToken.Token,StatusCodes.Status200OK);
    }
}