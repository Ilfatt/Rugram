using Auth.AutoMapper;
using Auth.Extensions;
using Auth.Grpc;
using Auth.Services;
using Auth.Services.BackgroundServices;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddEnvironmentVariables();
builder.Configuration.AddUserSecrets<Program>();

builder.ConfigurePostgresqlConnection();
builder.ConfigureRedisConnection();

builder.Services.AddMediatR(conf => conf.RegisterServicesFromAssemblyContaining<Program>());
builder.Services.AddGrpc();
builder.Services.AddAutoMapper(typeof(MapperProfile));

builder.Services.AddScoped<UserAuthHelperService>();

builder.Services.AddHostedService<DeleteOutdatedRefreshTokens>();
builder.Services.AddHostedService<DeleteOutdatedMailConfirmationTokens>();

var app = builder.Build();

await app.MigrateDb();

app.MapGrpcService<AuthGrpcService>();

app.Run();