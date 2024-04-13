using Contracts.RabbitMq;
using MassTransit;
using Posts.Services.S3;

namespace Posts.Consumers;

public class CreateBucketConsumer(IS3StorageService s3StorageService) : IConsumer<CreateBucketMessage>
{
	public async Task Consume(ConsumeContext<CreateBucketMessage> context)
	{
		if (!await s3StorageService.BucketExistAsync(context.Message.BucketIdentifier))
			await s3StorageService.CreateBucketAsync(context.Message.BucketIdentifier);
	}
}