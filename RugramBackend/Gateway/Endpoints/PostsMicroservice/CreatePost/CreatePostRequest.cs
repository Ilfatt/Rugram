using Swashbuckle.AspNetCore.Annotations;

namespace Gateway.Endpoints.PostsMicroservice.CreatePost;

public class CreatePostRequest()
{
	[SwaggerSchema("Описание поста")]
	public string Description { get; init; } = string.Empty;

	[SwaggerSchema("Фотки")]
	public IReadOnlyList<IFormFile> Photos { get; init; } = new List<IFormFile>();
}