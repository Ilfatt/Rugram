namespace Profile.Features.GetFeed;

public record GetFeedResponse(FeedPostDto[] NewsFeedPostDto);

public record FeedPostDto(
	Guid ProfileId,
	string ProfileName,
	string Description,
	DateTime DateOfCreation,
	Guid[] PhotoIds);