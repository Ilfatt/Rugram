namespace Auth.Extensions;

public static class ConfigurationExtensions
{
    public static int GetSlidingExpirationForRefreshToken(this IConfiguration configuration)
    {
        return int.Parse(configuration["Cache:SlidingExpirationForRefreshTokenInMinutes"]!);
    }
}