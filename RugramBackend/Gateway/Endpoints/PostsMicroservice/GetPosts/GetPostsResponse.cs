namespace Gateway.Endpoints.PostsMicroservice.GetPosts;

public record GetPostsResponse(PostDto[] Posts, int TotalPostsCount);

public record PostDto(Guid PostId, string Description, DateTime DateOfCreation, Guid[] PhotoIds);