using Swashbuckle.AspNetCore.Annotations;

namespace Gateway.Endpoints.ProfileMicroservice.GetFeed;

public record GetFeedResponse(
	[property: SwaggerSchema("Посты в новостной ленте. ")]
	FeedPostDto[] NewsFeedPostDto = null!);

public record FeedPostDto(
	[property: SwaggerSchema("Id профиля сделавшего пост")]
	Guid ProfileId,
	[property: SwaggerSchema("Название профиля сделавшего пост")]
	string ProfileName,
	[property: SwaggerSchema("Описание поста")]
	string Description,
	[property: SwaggerSchema("Дата создания поста")]
	DateTime DateOfCreation,
	[property: SwaggerSchema("Ids фотографий поста")]
	Guid[] PhotoIds);