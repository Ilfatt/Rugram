using Microsoft.AspNetCore.Identity;

namespace Auth.Models;

public class User
{
    public User()
    {
        Id = Guid.NewGuid();
    }
    
    public Guid Id { get; set; }
    public required string Email { get; set; }
    public required Role Role { get; set; }
    public required string Password { get; set; }

    #region Navigation

    public required List<RefreshToken> RefreshTokens { get; set; }

    #endregion
}

public enum Role
{
    User = 1,
    Admin = 2
}