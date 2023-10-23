namespace Gateway.Endpoints.AuthMicroservice.RegisterUser;

public record RegisterUserRequest(string MailConfirmationToken, string Email, string Password);