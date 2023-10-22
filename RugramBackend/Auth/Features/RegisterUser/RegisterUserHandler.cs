using Auth.Data;
using Auth.Data.Models;
using Auth.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Auth.Features.RegisterUser;

public class RegisterUserHandler : IRequestHandler<RegisterUserRequest, RegisterUserResponse>
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
        var mailConfirmationToken = await _dbContext.MailConfirmationTokens.AsNoTracking()
            .FirstOrDefaultAsync(token => token.Email == request.Email &&
                                          token.ValidTo > DateTime.UtcNow &&
                                          token.Value == request.MailConfirmationToken, cancellationToken);

        if (mailConfirmationToken == null)
        {
            return new RegisterUserResponse("", "", StatusCodes.Status404NotFound);
        }

        var user = new User
        {
            Email = request.Email,
            Password = request.Password,
            RefreshTokens = new List<RefreshToken>(),
            Role = Role.User,
        };

        var result = await _userAuthHelperService.CreateRefreshToken(user.Id);
        user.RefreshTokens.Add(result.RefreshToken);

        _dbContext.Users.Add(user);
        _dbContext.RefreshTokens.Add(result.RefreshToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        var jwtToken = _userAuthHelperService.GenerateJwtToken(user.Id, user.Role);

        return new RegisterUserResponse(
            jwtToken,
            result.UnhashedTokenValue,
            StatusCodes.Status200OK);
    }
}