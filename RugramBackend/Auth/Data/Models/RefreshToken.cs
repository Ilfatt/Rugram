namespace Auth.Data.Models;

public class RefreshToken
{
    public RefreshToken()
    {
        Id = Guid.NewGuid();
    }

    public Guid Id { get; init; }
    public required string Value { get; init; }
    public required DateTime ValidTo { get; init; }

    #region Navigation

    public User? User { get; set; }
    public required Guid UserId { get; init; }

    #endregion
}