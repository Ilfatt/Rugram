using Contracts;

namespace Auth.Features.RegisterUser;

public record RegisterUserRequest
    (string MailConfirmationToken, string Email, string Password) : IGrpcRequest<GrpcResult<RegisterUserResponse>>;