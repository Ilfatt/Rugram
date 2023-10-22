using Auth.AutoMapper;
using Auth.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.ConfigureDbConnection();
builder.ConfigureRedisConnection();

builder.Services.AddMediatR(conf => conf.RegisterServicesFromAssemblyContaining<Program>());
builder.Services.AddGrpc();
builder.Services.AddAutoMapper(typeof(MapperProfile));

var app = builder.Build();

await app.MigrateDb();

app.MapGet("/", () => "Hello World!");

app.Run();