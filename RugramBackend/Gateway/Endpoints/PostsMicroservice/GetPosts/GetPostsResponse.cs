using Swashbuckle.AspNetCore.Annotations;

namespace Gateway.Endpoints.PostsMicroservice.GetPosts;

public record GetPostsResponse(
	[SwaggerSchema("Посты")]
	ProfilePostDto[] Posts,
	[SwaggerSchema("Общее колличество постов")]
	int TotalPostsCount);

public record ProfilePostDto(
	[SwaggerSchema("Id поста")]
	Guid PostId,
	[SwaggerSchema("Описание фотки")]
	string Description,
	[SwaggerSchema("Дата создания поста")]
	DateTime DateOfCreation,
	[SwaggerSchema("Ids фоток данного поста")]
	Guid[] PhotoIds);
	