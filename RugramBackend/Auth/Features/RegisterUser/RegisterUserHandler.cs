using Auth.Data;
using Auth.Data.Models;
using Auth.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static Auth.Services.UserAuthHelperService;

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
        var userWithThisEmailExist = await _dbContext.Users.AsNoTracking()
            .AnyAsync(user => user.Email == request.Email, cancellationToken: cancellationToken);

        if (userWithThisEmailExist)
        {
            return new RegisterUserResponse("", "", StatusCodes.Status409Conflict);
        }

        var hashedToken = HashSha256(request.MailConfirmationToken);
        var mailConfirmationToken = await _dbContext.MailConfirmationTokens.AsNoTracking()
            .FirstOrDefaultAsync(token => token.Email == request.Email &&
                                          token.ValidTo > DateTime.UtcNow &&
                                          token.Value == hashedToken, cancellationToken);

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