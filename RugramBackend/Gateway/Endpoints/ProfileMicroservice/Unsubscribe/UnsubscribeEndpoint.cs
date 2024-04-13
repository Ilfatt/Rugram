using AutoMapper;
using Gateway.Contracts;
using Gateway.Extensions;
using Swashbuckle.AspNetCore.Annotations;
using static ProfileMicroservice;


namespace Gateway.Endpoints.ProfileMicroservice.Unsubscribe;

public class UnsubscribeEndpoint : IEndpoint
{
	public void AddRoute(IEndpointRouteBuilder app)
	{
		app.MapPut("profile/unsubscribe/{idOfProfileSubscribedTo}", async (
				Guid idOfProfileSubscribedTo,
				ProfileMicroserviceClient profileClient,
				IHttpContextAccessor httpContextAccessor,
				CancellationToken cancellationToken) =>
			{
				var response = await profileClient.UnsubscribeAsync(
					new UnsubscribeGrpcRequest
					{
						IdOfProfileUnsubscribedTo = idOfProfileSubscribedTo.ToString(), 
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
			.WithOpenApi()
			.WithTags("Profile")
			.WithSummary("Отписаться от профиля")
			.WithDescription("Доступ: авторизованные пользователи")
			.WithMetadata(
				new SwaggerResponseAttribute(StatusCodes.Status500InternalServerError),
				new SwaggerResponseAttribute(
					StatusCodes.Status400BadRequest,
					$"Id профиля, от которого отписываются, равен: '{Guid.Empty}' "),
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