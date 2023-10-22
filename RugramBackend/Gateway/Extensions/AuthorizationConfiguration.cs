using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace Gateway.Extensions;

public static class AuthorizationConfiguration
{
    /// <summary>
    ///     Добавление авторизации
    /// </summary>
    /// <param name="builder">WebApplicationBuilder</param>
    public static void AddAuthorization(this WebApplicationBuilder builder)
    {
        var authConf = builder.Configuration.GetSection("AuthOptions");

        builder.Services.AddAuthorization();
        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                if (builder.Environment.IsDevelopment()) options.RequireHttpsMetadata = false;

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = authConf.GetValue<string>("Issuer"),
                    ValidateAudience = true,
                    ValidAudience = authConf.GetValue<string>("Audience"),
                    ValidateLifetime = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.ASCII.GetBytes(authConf.GetValue<string>("JwtSecretKey")!)),
                    ValidateIssuerSigningKey = true
                };
            });
    }
}