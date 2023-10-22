using Auth.Data;
using Auth.Models;
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
        await using var scope = _serviceProvider.CreateAsyncScope();
        await using var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        const string command = $"DELETE FROM \"{nameof(AppDbContext.RefreshTokens)}\" AS RT " +
                               $"WHERE RT.\"{nameof(RefreshToken.ValidTo)}\" > now()";

        while (!cancellationToken.IsCancellationRequested)
        {
            await dbContext.Database.ExecuteSqlRawAsync(
                command,
                cancellationToken: cancellationToken);

            await Task.Delay(1000 * 60 * 5, cancellationToken);
        }
    }
}