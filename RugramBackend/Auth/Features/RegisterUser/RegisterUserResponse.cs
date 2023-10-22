namespace Auth.Features.RegisterUser;

public record RegisterUserResponse(
    string JwtToken,
    string RefreshToken,
    int StatusCode);