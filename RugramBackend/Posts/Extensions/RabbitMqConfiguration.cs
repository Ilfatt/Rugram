using MassTransit;
using Posts.Consumers;

namespace Posts.Extensions;

public static class RabbitMqConfiguration
{
	public static async Task AddMasstransitRabbitMq(this WebApplicationBuilder builder)
	{
		await Task.Delay(1000 * 10);

		builder.Services.AddMassTransit(config =>
		{
			config.AddConsumer<CreateBucketConsumer>();
			config.AddConsumer<DeleteBucketConsumer>();

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