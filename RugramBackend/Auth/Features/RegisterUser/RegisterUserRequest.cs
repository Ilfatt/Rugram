using MediatR;

namespace Auth.Features.RegisterUser;

public record RegisterUserRequest(string Email, string Password) : IRequest<RegisterUserResponse>;