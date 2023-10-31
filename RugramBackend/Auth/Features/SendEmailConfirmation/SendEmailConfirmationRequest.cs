using Contracts;

namespace Auth.Features.SendEmailConfirmation;

public record SendEmailConfirmationRequest(string Email) : IGrpcRequest<SendEmailConfirmationResponse>;