using Swashbuckle.AspNetCore.Annotations;

namespace Gateway.Endpoints.ProfileMicroservice.Subscribe;

public record SubscribeRequest(
	[property: SwaggerSchema("Id подписчика")]
	Guid SubscriberId,
	[property: SwaggerSchema("Id профиля на который подписываются")]
	Guid IdOfProfileSubscribedTo);