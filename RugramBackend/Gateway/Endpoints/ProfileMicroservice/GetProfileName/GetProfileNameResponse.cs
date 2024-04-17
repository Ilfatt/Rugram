using Swashbuckle.AspNetCore.Annotations;

namespace Gateway.Endpoints.ProfileMicroservice.GetProfileName;

public record GetProfileNameResponse(
	[SwaggerSchema("Ник профиля")]
	string ProfileName);