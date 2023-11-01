using Auth;
using Auth.AutoMapper;
using Auth.Extensions;
using Auth.Features.SendEmailConfirmation;
using Auth.Grpc;
using Auth.Services;
using Auth.Services.BackgroundServices;
using Auth.Services.Infrastructure.EmailSenderService;
using Infrastructure.MediatR;
using FluentValidation;
using Infrastructure.MediatR.Behaviors;
using Infrastructure.MediatR.Extensions;
using MediatR;
using Microsoft.AspNetCore.Server.Kestrel.Core;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.ConfigureKestrel(serverOptions =>
{
    serverOptions.ConfigureEndpointDefaults(options => options.Protocols = HttpProtocols.Http2);
});

builder.Configuration.AddEnvironmentVariables();
builder.Configuration.AddUserSecrets<Program>();

builder.ConfigurePostgresqlConnection();
builder.ConfigureRedisConnection();

builder.Services.AddMediatR(conf => conf.RegisterServicesFromAssemblyContaining<Program>());
builder.Services.AddBehaviorsReturningGrpcResultFromAssembly(
    typeof(Program).Assembly,
    typeof(ValidationBehavior<,>));
builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);
builder.Services.AddGrpc();
builder.Services.AddAutoMapper(typeof(MapperProfile));

builder.Services.AddScoped<UserAuthHelperService>();
builder.Services.AddSingleton<IEmailSenderService, EmailSenderService>();

builder.Services.AddHostedService<DeleteOutdatedRefreshTokens>();
builder.Services.AddHostedService<DeleteOutdatedMailConfirmationTokens>();

var app = builder.Build();

await app.MigrateDb();

app.MapGrpcService<AuthGrpcService>();

app.Run();