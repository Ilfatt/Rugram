using Swashbuckle.AspNetCore.Annotations;

namespace Gateway.Endpoints.PostsMicroservice.CreatePost;

public record CreatePostRequest(
	[property: SwaggerSchema("Описание поста")]
	string Description,
	[property: SwaggerSchema("Фотки")]
	List<IFormFile> Photos);
	