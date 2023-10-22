using Auth.Data;
using Auth.Data.Models;
using Auth.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Auth.Features.SendEmailConfirmation;

public class SendEmailConfirmationHandler : IRequestHandler<SendEmailConfirmationRequest, SendEmailConfirmationResponse>
{
    private readonly UserAuthHelperService _userAuthHelperService;
    private readonly AppDbContext _dbContext;
    private readonly IConfiguration _configuration;

    public SendEmailConfirmationHandler(
        AppDbContext dbContext,
        UserAuthHelperService userAuthHelperService,
        IConfiguration configuration)
    {
        _configuration = configuration;
        _userAuthHelperService = userAuthHelperService;
        _dbContext = dbContext;
    }

    public async Task<SendEmailConfirmationResponse> Handle(SendEmailConfirmationRequest request,
        CancellationToken cancellationToken)
    {
        var countUsersWithSameEmail = await _dbContext.Users.AsNoTracking()
            .CountAsync(user => user.Email == request.Email, cancellationToken);

        if (countUsersWithSameEmail > 0)
        {
            return new SendEmailConfirmationResponse(StatusCodes.Status409Conflict);
        }

        var token = UserAuthHelperService.GenerateSecureToken();
        var mailConfirmation = new MailConfirmationToken
        {
            Email = request.Email,
            Value = UserAuthHelperService.HashSha256(token),
            ValidTo = DateTime.UtcNow + TimeSpan.FromHours(int.Parse(
                _configuration["MailConfirmationToken__LifetimeInHours"]!))
        };

        _dbContext.MailConfirmationTokens.Add(mailConfirmation);
        await _dbContext.SaveChangesAsync(cancellationToken);

        var messageBody = FirstPartMessageBody +
                          _configuration["Domain__AppUrl"] +
                          $"/auth/confirm-email?token={token}&email={request.Email}" +
                          SecondPartMessageBody;

        _userAuthHelperService.SendMessage(MessageSubject, messageBody, request.Email);

        return new SendEmailConfirmationResponse(StatusCodes.Status202Accepted);
    }

    private const string MessageSubject = "Потверждение почты";

    private const string FirstPartMessageBody = @"
    <!DOCTYPE html>
    <html lang=""en"">
    <head>
        <meta charset=""UTF-8"">
        <meta http-equiv=""X-UA-Compatible"" content=""IE=edge"">
        <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
        <title>Подтверждение почты</title>
        <style>
            body {
                background-color: #F0F0F0;
                font-family: Arial, sans-serif;
            }

            .container {
                background-color: #FFFFFF;
                border-radius: 5px;
                box-shadow: 0 2px 5px rgba(0, 0, 0, 0.1);
                margin: 20px auto;
                padding: 20px;
                max-width: 400px;
            }

            h1 {
                font-size: 20px;
                margin-bottom: 20px;
                text-align: center;
            }

            p {
                margin-bottom: 20px;
            }

            .button {
                background-color: #5BC0DE;
                border-radius: 5px;
                color: #FFFFFF;
                display: inline-block;
                font-size: 16px;
                padding: 10px 20px;
                text-decoration: none;
            }

            .button:hover {
                background-color: #449DCA;
            }
        </style>
    </head>
    <body>
        <div class=""container"">
            <h1>Подтверждение почты</h1>
            <p>Спасибо вам за выбор Rugram! Пожалуйста, нажмите на кнопку ниже, чтобы продолжить регистрацию</p>
            <a class=""button"" href=";

    private const string SecondPartMessageBody = @""">Продолжить регистрацию</a>
        </div>
    </body>
    </html>";
}