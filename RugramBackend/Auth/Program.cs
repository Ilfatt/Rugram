using Auth.AutoMapper;
using Auth.Data;
using Auth.Extensions;
using Auth.Models;
using Auth.Services;
using Auth.Services.BackgroundServices;

var builder = WebApplication.CreateBuilder(args);

builder.ConfigureDbConnection();
builder.ConfigureRedisConnection();

builder.Services.AddMediatR(conf => conf.RegisterServicesFromAssemblyContaining<Program>());
builder.Services.AddGrpc();
builder.Services.AddAutoMapper(typeof(MapperProfile));
builder.Services.AddScoped<UserAuthHelperService>();
builder.Services.AddHostedService<DeleteOutdatedRefreshTokens>();

var app = builder.Build();

await app.MigrateDb();

app.MapGet("/", () => "Hello World!");

app.Run();