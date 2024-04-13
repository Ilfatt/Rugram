using FluentValidation;
using Infrastructure.MediatR.Extensions;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Profile.AutoMapper;
using Profile.Extensions;
using Profile.Grpc.ProfileForAuthService;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.ConfigureKestrel(serverOptions =>
{
	serverOptions.ConfigureEndpointDefaults(options => options.Protocols = HttpProtocols.Http2);
});

builder.Configuration.AddEnvironmentVariables();

builder.Services.AddMediatR(conf => conf.RegisterServicesFromAssemblyContaining<Program>());
builder.Services.AddValidationBehaviorsFromAssembly(typeof(Program).Assembly);
builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);
builder.Services.AddAutoMapper(typeof(MapperProfile));
builder.Services.AddGrpc();

builder.ConfigurePostgresqlConnection();

var app = builder.Build();

await app.MigrateDbAsync();

app.MapGrpcService<ProfileForAuthGrpcService>();

app.Run();