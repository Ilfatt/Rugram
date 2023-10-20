using Auth.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.ConfigureDbConnection();

var app = builder.Build();

await app.MigrateDb();

app.MapGet("/", () => "Hello World!");

app.Run();