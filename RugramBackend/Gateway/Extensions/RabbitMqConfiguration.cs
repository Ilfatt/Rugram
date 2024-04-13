using MassTransit;

namespace Gateway.Extensions;

public static class RabbitMqConfiguration
{
	public static async Task AddMasstransitRabbitMq(this WebApplicationBuilder builder)
	{
		await Task.Delay(1000 * 15);

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