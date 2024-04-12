namespace Auth.Data.Models;

public class User
{
	public Guid Id { get; init; } = Guid.NewGuid();
	public required string Email { get; init; }
	public required Role Role { get; init; }
	public required string Password { get; set; }

	#region Navigation

	// ReSharper disable once CollectionNeverQueried.Global
	public required List<RefreshToken> RefreshTokens { get; init; }

	#endregion
}

public enum Role
{
	User = 1,
	Admin = 2
}