namespace Posts.Features.GetPosts;

public record GetPostsResponse(IReadOnlyList<PostDto> Posts, int TotalPostsCount);

public record PostDto(Guid PostId, string Description, DateTime DateOfCreation, Guid[] PhotoIds);