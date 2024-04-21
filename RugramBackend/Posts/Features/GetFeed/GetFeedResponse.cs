namespace Posts.Features.GetFeed;

public record GetFeedResponse(FeedPostDto[] NewsFeedPostDto);


public record FeedPostDto(
	Guid ProfileId,
	string Description,
	DateTime DateOfCreation,
	Guid[] PhotoIds);