using Swashbuckle.AspNetCore.Annotations;

namespace Gateway.Endpoints.ProfileMicroservice.GetFeed;

public record GetFeedResponse(
	[SwaggerSchema("Посты в новостной ленте. ")]
	FeedPostDto[] NewsFeedPostDto = null!);

public record FeedPostDto(
	[SwaggerSchema("Id профиля сделавшего пост")]
	Guid ProfileId,
	[SwaggerSchema("Название профиля сделавшего пост")]
	string ProfileName,
	[SwaggerSchema("Описание поста")]
	string Description,
	[SwaggerSchema("Дата создания поста")]
	DateTime DateOfCreation,
	[SwaggerSchema("Ids фотографий поста")]
	Guid[] PhotoIds);