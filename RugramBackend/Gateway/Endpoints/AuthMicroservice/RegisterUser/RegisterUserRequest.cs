namespace Gateway.Endpoints.AuthMicroservice.RegisterUser;

public record RegisterUserRequest(string EmailConfirmationToken, string Email, string Password);