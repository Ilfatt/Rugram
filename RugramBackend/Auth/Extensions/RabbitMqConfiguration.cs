using MassTransit;

namespace Auth.Extensions;

public static class RabbitMqConfiguration
{
	public static async Task AddMasstransitRabbitMq(this WebApplicationBuilder builder, int attemptsCount = 20)
	{
		while (attemptsCount >= 0)
		{
			try
			{
				ConfigureRabbitMq(builder);
				break;
			}
			catch (Exception)
			{
				await Task.Delay(1000);
				attemptsCount--;
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