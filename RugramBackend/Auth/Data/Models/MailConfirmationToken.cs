namespace Auth.Data.Models;

public class MailConfirmationToken
{
    public MailConfirmationToken()
    {
        Id = Guid.NewGuid();
    }

    public Guid Id { get; init; }
    public required string Email { get; init; }
    public required string Value { get; init; }
    public required DateTime ValidTo { get; init; }
}