namespace Profile.Data.Models;

public class UserProfile(Guid id, string profileName)
{
	public Guid Id { get; init; } = id;
	public string ProfileName { get; init; } = profileName;

	#region Navigation

	public required List<UserProfile> Subscribers { get; init; }
	public required List<UserProfile> SubscribedTo { get; init; }

	#endregion
}