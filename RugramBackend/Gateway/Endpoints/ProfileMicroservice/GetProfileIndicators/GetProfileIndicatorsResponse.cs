using Swashbuckle.AspNetCore.Annotations;

namespace Gateway.Endpoints.ProfileMicroservice.GetProfileIndicators;

public record GetProfileIndicatorsResponse(
	[SwaggerSchema("Количество подписчиков")]
	int SubscribersCount,
	[SwaggerSchema("Количество подписок на аккаунт")]
	int SubscriptionsCount);