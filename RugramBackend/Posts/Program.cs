using Minio;
using Posts.Extensions;
using Posts.Services.S3;

var builder = WebApplication.CreateBuilder(args);

builder.ConfigurePostgresqlConnection();
await builder.AddMasstransitRabbitMq();

builder.Services.AddSingleton<IS3StorageService, MinioS3StorageService>();
builder.Services.AddMinio(configuration =>
{
	configuration.WithSSL(false);
	configuration.WithTimeout(int.Parse(builder.Configuration["MinioS3:Timeout"]!));
	configuration.WithEndpoint(builder.Configuration["MinioS3:Endpoint"]);
	configuration.WithCredentials(
		builder.Configuration["MinioS3:AccessKey"]!,
		builder.Configuration["MinioS3:SecretKey"]!);
});

var app = builder.Build();

await app.MigrateDbAsync();

app.Run();