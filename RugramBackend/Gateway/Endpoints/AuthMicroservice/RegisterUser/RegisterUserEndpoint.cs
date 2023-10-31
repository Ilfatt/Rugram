using AutoMapper;
using Gateway.Contracts;
using static AuthMicroservice;

namespace Gateway.Endpoints.AuthMicroservice.RegisterUser;

public class RegisterUserEndpoint : IEndpoint
{
    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapPost("auth/registr-user", async (
                RegisterUserRequest request,
                AuthMicroserviceClient authClient,
                IMapper mapper) =>
            {
                var response = await authClient.RegisterUserAsync(
                    mapper.Map<RegisterUserGrpcRequest>(request));

                return response.HttpStatusCode switch
                {
                    200 => Results.Ok(mapper.Map<RegisterUserResponse>(response)),
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