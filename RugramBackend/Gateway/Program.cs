using Gateway.AutoMapper;
using Gateway.Extensions;

var builder = WebApplication.CreateBuilder(args);

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

app.Run();