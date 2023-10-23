using Auth.Data;
using Auth.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Auth.Services.BackgroundServices;

public class DeleteOutdatedRefreshTokens : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;

    public DeleteOutdatedRefreshTokens(
        IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        const string command = $"DELETE FROM \"{nameof(AppDbContext.RefreshTokens)}\" AS t " +
                               $"WHERE t.\"{nameof(RefreshToken.ValidTo)}\" < now()";

        while (!cancellationToken.IsCancellationRequested)
        {
            await using var scope = _serviceProvider.CreateAsyncScope();
            await using var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            await dbContext.Database.ExecuteSqlRawAsync(
                command,
                cancellationToken: cancellationToken);

            await Task.Delay(1000 * 60 * 60, cancellationToken);
        }
    }
}