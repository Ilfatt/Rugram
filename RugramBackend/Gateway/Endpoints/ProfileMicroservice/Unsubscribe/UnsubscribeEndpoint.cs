using AutoMapper;
using Gateway.Contracts;
using Swashbuckle.AspNetCore.Annotations;
using static ProfileMicroservice;


namespace Gateway.Endpoints.ProfileMicroservice.Unsubscribe;

public class UnsubscribeEndpoint : IEndpoint
{
	public void AddRoute(IEndpointRouteBuilder app)
	{
		app.MapPut("profile/unsubscribe", async (
				UnsubscribeRequest request,
				IMapper mapper,
				ProfileMicroserviceClient profileClient,
				CancellationToken cancellationToken) =>
			{
				var response = await profileClient.UnsubscribeAsync(
					mapper.Map<UnsubscribeGrpcRequest>(request), cancellationToken: cancellationToken);
				
				return response.HttpStatusCode switch
				{
					204 => Results.NoContent(),
					400 => Results.BadRequest(),
					404 => Results.NotFound(),
					_ => Results.Problem(statusCode: 500)
				};
			})
			.RequireAuthorization()
			.WithOpenApi()
			.WithTags("Profile")
			.WithSummary("Отписаться от профиля")
			.WithDescription("Доступ: авторизованные пользователи")
			.WithMetadata(
				new SwaggerResponseAttribute(StatusCodes.Status500InternalServerError),
				new SwaggerResponseAttribute(
					StatusCodes.Status400BadRequest,
					$"Один из id равен {Guid.Empty}"),
				new SwaggerResponseAttribute(
					StatusCodes.Status401Unauthorized,
					"Пользователь не авторизован"),
				new SwaggerResponseAttribute(
					StatusCodes.Status404NotFound,
					"Пользователь, как минимум с одним из id, не найден"),
				new SwaggerResponseAttribute(
					StatusCodes.Status204NoContent,
					"Отписка произошла успешно или пользоваетль уже был отписан")
			);
	}
}