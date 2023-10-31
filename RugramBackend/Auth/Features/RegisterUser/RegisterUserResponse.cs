using Contracts;

namespace Auth.Features.RegisterUser;

public record RegisterUserResponse(
    string RefreshToken,
    string JwtToken,
    int HttpStatusCode) : IGrpcResponse;