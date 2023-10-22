using MediatR;

namespace Auth.Features.SendEmailConfirmation;

public record SendEmailConfirmationRequest(string Email) : IRequest<SendEmailConfirmationResponse>;