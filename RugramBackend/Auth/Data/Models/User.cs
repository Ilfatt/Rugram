namespace Auth.Data.Models;

public class User
{
    public User()
    {
        Id = Guid.NewGuid();
    }

    public Guid Id { get; init; }
    public required string Email { get; init; }
    public required Role Role { get; init; }
    public required string Password { get; set; }

    #region Navigation

    public required List<RefreshToken> RefreshTokens { get; init; }

    #endregion
}

public enum Role
{
    User = 1,
    Admin = 2
}