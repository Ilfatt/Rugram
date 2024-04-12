using Profile.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.ConfigurePostgresqlConnection();

var app = builder.Build();

await app.MigrateDbAsync();

app.Run();