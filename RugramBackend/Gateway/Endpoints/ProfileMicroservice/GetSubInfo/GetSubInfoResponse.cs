using Swashbuckle.AspNetCore.Annotations;

namespace Gateway.Endpoints.ProfileMicroservice.GetSubInfo;

public class GetSubInfoResponse(
	[SwaggerSchema("Подписан ли другой профиль(профиль который в запросе) на меня")]
	bool OtherProfileSubscribedToThisProfile,
	[SwaggerSchema("Подписан ли я на другой профиль(профиль который в запросе)")]
	bool ThisProfileSubscribedToOtherProfile);