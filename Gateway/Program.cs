using Gateway.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.AddSwagger();
builder.AddAuthorization();

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