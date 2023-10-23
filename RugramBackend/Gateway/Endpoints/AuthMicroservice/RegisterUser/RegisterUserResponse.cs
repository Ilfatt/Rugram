namespace Gateway.Endpoints.AuthMicroservice.RegisterUser;

public record RegisterUserResponse(string JwtToken, string RefreshToken);