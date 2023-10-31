using Contracts;

namespace Auth.Features.SendEmailConfirmation;

public record SendEmailConfirmationResponse(int HttpStatusCode) : IGrpcResponse;