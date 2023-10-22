using MediatR;

namespace Auth.Features.RegisterUser;

public record RegisterUserRequest
    (string MailConfirmationToken, string Email, string Password) : IRequest<RegisterUserResponse>;