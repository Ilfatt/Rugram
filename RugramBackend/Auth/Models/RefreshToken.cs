namespace Auth.Models;

public class RefreshToken
{
    public RefreshToken()
    {
        Id = Guid.NewGuid();
    }

    public Guid Id { get; set; }
    public required string Token { get; set; }
    public required DateTime ValidTo { get; set; }

    #region Navigation

    public User? User { get; set; }
    public required Guid UserId { get; set; }

    #endregion
}