using Gateway.Contracts;
using Gateway.Extensions;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using static ProfileMicroservice;


namespace Gateway.Endpoints.ProfileMicroservice.Unsubscribe;

public class UnsubscribeEndpoint : IEndpoint
{
	public void AddRoute(IEndpointRouteBuilder app)
	{
		app.MapPut("profile/unsubscribe/{nameOfProfileUnsubscribedTo}", async (
				string nameOfProfileUnsubscribedTo,
				ProfileMicroserviceClient profileClient,
				[FromServices] IHttpContextAccessor httpContextAccessor,
				CancellationToken cancellationToken) =>
			{
				var response = await profileClient.UnsubscribeAsync(
					new UnsubscribeGrpcRequest
					{
						NameOfProfileUnsubscribedTo = nameOfProfileUnsubscribedTo,
						SubscriberId = httpContextAccessor.HttpContext!.GetUserId().ToString()
					},
					cancellationToken: cancellationToken);

				return response.HttpStatusCode switch
				{
					204 => Results.NoContent(),
					400 => Results.BadRequest(),
					404 => Results.NotFound(),
					_ => Results.Problem(statusCode: 500)
				};
			})
			.RequireAuthorization()
			.WithOpenApi(generatedOperation =>
			{
				var parameter = generatedOperation.Parameters[0];
				parameter.Description = "Ник профиля, от которого отписываются";
				return generatedOperation;
			})
			.WithTags("Profile")
			.WithSummary("Отписаться от профиля")
			.WithDescription("Доступ: авторизованные пользователи")
			.WithMetadata(
				new SwaggerResponseAttribute(StatusCodes.Status500InternalServerError),
				new SwaggerResponseAttribute(
					StatusCodes.Status400BadRequest,
					"Не пройдена валидация. nameOfProfileSubscribedTo length от 5 до 25"),
				new SwaggerResponseAttribute(
					StatusCodes.Status401Unauthorized,
					"Пользователь не авторизован"),
				new SwaggerResponseAttribute(
					StatusCodes.Status404NotFound,
					"Профиль, от которого отписываются, не найден"),
				new SwaggerResponseAttribute(
					StatusCodes.Status204NoContent,
					"Отписка произошла успешно или пользоваетль уже был отписан")
			);
	}
}