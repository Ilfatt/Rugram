using Swashbuckle.AspNetCore.Annotations;

namespace Gateway.Endpoints.ProfileMicroservice.GetProfileRecommendations;

public record GetProfileRecommendationsResponse(
	[SwaggerSchema("Профили")]
	ProfileDto[] Profiles);

public record ProfileDto(
	[SwaggerSchema("Id профиля")]
	Guid Id,
	[SwaggerSchema("Ник профиля")]
	string ProfileName);