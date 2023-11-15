using Gateway.AutoMapper;
using Gateway.Extensions;
using Microsoft.AspNetCore.Server.Kestrel.Core;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.ConfigureKestrel(serverOptions =>
{
    serverOptions.ConfigureEndpointDefaults(options => options.Protocols = HttpProtocols.Http2);
});

builder.Services.AddCors();

builder.Configuration.AddEnvironmentVariables();
builder.Configuration.AddUserSecrets<Program>();

builder.AddSwagger();
builder.AddAuthorization();

builder.Services.AddGrpc();
builder.AddGrpcClients();

builder.Services.AddAutoMapper(typeof(MapperProfile));

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.RouteEndpoints();

app.UseCors(option =>
{
    option.AllowAnyHeader();
    option.AllowAnyHeader();
    option.AllowAnyMethod();
});

app.Run();