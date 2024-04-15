using Swashbuckle.AspNetCore.Annotations;

namespace Gateway.Endpoints.ProfileMicroservice.GetUserProfilePhoto;

public record GetProfilePhotoResponse(
	[SwaggerSchema("Фото профиля")]
	byte[] ProfilePhoto);