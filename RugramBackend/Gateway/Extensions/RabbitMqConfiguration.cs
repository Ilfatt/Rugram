using MassTransit;

namespace Gateway.Extensions;

public static class RabbitMqConfiguration
{
	public static async Task AddMasstransitRabbitMq(this WebApplicationBuilder builder, int attemptsCount = 20)
	{
		while (attemptsCount >= 0)
		{
			try
			{
				ConfigureRabbitMq(builder);
				return;
			}
			catch (Exception)
			{
				await Task.Delay(1000);
				attemptsCount--;
				
				if (attemptsCount <= 0)
					throw;
			}
		}
	}

	private static void ConfigureRabbitMq(WebApplicationBuilder builder)
	{
		builder.Services.AddMassTransit(config =>
		{
			config.UsingRabbitMq((ctx, cfg) =>
			{
				cfg.Host(
					$"amqp://{builder.Configuration["RabbitMqConfig:Username"]}:{builder.Configuration["RabbitMqConfig:Password"]}" +
					$"@{builder.Configuration["RabbitMqConfig:Hostname"]}:{builder.Configuration["RabbitMqConfig:Port"]}");
				cfg.ConfigureEndpoints(ctx);
			});
		});
	}
}