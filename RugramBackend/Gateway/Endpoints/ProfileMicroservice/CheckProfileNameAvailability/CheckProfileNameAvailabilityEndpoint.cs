using AutoMapper;
using Gateway.Contracts;
using Swashbuckle.AspNetCore.Annotations;

namespace Gateway.Endpoints.ProfileMicroservice.CheckProfileNameAvailability;

public class CheckProfileNameAvailabilityEndpoint : IEndpoint
{
	public void AddRoute(IEndpointRouteBuilder app)
	{
		app.MapGet("profile/checkProfileNameAvailability/{profileName}", (
				string profileName,
				IMapper mapper,
				CancellationToken cancellationToken) => Results.Ok())
			.AllowAnonymous()
			.WithOpenApi(generatedOperation =>
			{
				var parameter = generatedOperation.Parameters[0];
				parameter.Description = "Ник профиля";
				return generatedOperation;
			})
			.WithTags("Profile")
			.WithSummary("Проверка доступности ника профиля")
			.WithDescription("Доступ: все")
			.WithMetadata(
				new SwaggerResponseAttribute(StatusCodes.Status500InternalServerError),
				new SwaggerResponseAttribute(
					StatusCodes.Status400BadRequest,
					"Не пройдена валидация. Ник - должен быть больше 5 символов и меньше 25"),
				new SwaggerResponseAttribute(
					StatusCodes.Status409Conflict,
					"Ник занят"),
				new SwaggerResponseAttribute(
					StatusCodes.Status200OK,
					"Ник свободен")
			);
	}
}