using AutoMapper;
using Gateway.Contracts;
using static AuthMicroservice;

namespace Gateway.Endpoints.AuthMicroservice.SendEmailConfirmation;

public class SendEmailConfirmationEndpoint : IEndpoint
{
    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapPost("auth/confirm-email", async (
                IMapper mapper,
                AuthMicroserviceClient authClient,
                SendEmailConfirmationRequest request) =>
            {
                var response = await authClient.SendEmailConfirmationAsync(
                    mapper.Map<SendEmailConfirmationGrpcRequest>(request));

                return response.HttpStatusCode switch
                {
                    202 => Results.Accepted(),
                    404 => Results.NotFound(),
                    409 => Results.Conflict(),
                    _ => Results.Problem(statusCode: 500)
                };
            })
            .AllowAnonymous()
            .WithOpenApi()
            .WithTags("auth");
    }
}