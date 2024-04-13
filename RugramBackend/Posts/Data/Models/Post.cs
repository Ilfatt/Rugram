namespace Posts.Data.Models;

public class Post(Guid profileId, Guid postId, string description)
{
	public Guid Id { get; init; } = postId;
	public Guid ProfileId { get; init; } = profileId;
	public DateTime DateOfCreation { get; init; } = DateTime.UtcNow;
	public string Description { get; set; } = description;

	#region Navigation

	public List<Photo> Photos { get; set; } = null!;

	#endregion
}