using Swashbuckle.AspNetCore.Annotations;

namespace Gateway.Endpoints.ProfileMicroservice.EditProfilePhoto;

public record EditProfilePhotoRequest(
	[SwaggerSchema("Фотка для профиля")]
	IFormFile ProfilePhoto);