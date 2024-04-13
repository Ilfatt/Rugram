using Swashbuckle.AspNetCore.Annotations;

namespace Gateway.Endpoints.ProfileMicroservice.Unsubscribe;

public record UnsubscribeRequest(
	[property: SwaggerSchema("Id подписчика")]
	Guid SubscriberId,
	[property: SwaggerSchema("Id профиля от которого отписываются")]
	Guid IdOfProfileUnsubscribedTo);