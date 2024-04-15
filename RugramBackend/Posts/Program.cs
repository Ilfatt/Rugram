using Infrastructure.S3;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Minio;
using Posts.AutoMapper;
using Posts.Extensions;
using Posts.Grpc.ProfileService;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.ConfigureKestrel(serverOptions =>
{
	serverOptions.ConfigureEndpointDefaults(options => options.Protocols = HttpProtocols.Http2);
});

builder.ConfigurePostgresqlConnection();
builder.AddMasstransitRabbitMq();

builder.Services.AddGrpc();
builder.Services.AddSingleton<IS3StorageService, MinioS3StorageService>();
builder.Services.AddAutoMapper(typeof(MapperProfile));
builder.Services.AddMediatR(conf => conf.RegisterServicesFromAssemblyContaining<Program>());
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

app.MapGrpcService<PostGrpcService>();

await Task.Delay(1000 * 15);

app.Run();