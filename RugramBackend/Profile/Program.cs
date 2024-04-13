using FluentValidation;
using Infrastructure.MediatR.Extensions;
using Profile.Extensions;
using Profile.Grpc.ProfileForAuthService;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddEnvironmentVariables();

builder.Services.AddMediatR(conf => conf.RegisterServicesFromAssemblyContaining<Program>());
builder.Services.AddValidationBehaviorsFromAssembly(typeof(Program).Assembly);
builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);
builder.Services.AddGrpc();

builder.ConfigurePostgresqlConnection();

var app = builder.Build();

await app.MigrateDbAsync();

app.MapGrpcService<ProfileForAuthGrpcService>();

app.Run();	