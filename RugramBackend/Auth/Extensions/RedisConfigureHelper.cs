namespace Auth.Extensions;

public static class RedisConfigureHelper
{
    /// <summary>
    /// Конфигурация подключения к redis
    /// </summary>
    /// <param name="builder">WebApplicationBuilder</param>
    public static void ConfigureRedisConnection(this WebApplicationBuilder builder)
    {
        builder.Services.AddStackExchangeRedisCache(options =>
            options.Configuration = builder.Configuration.GetConnectionString("Redis"));
    }
}