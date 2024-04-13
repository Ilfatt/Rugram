using Contracts.RabbitMq;
using MassTransit;
using Posts.Services.S3;

namespace Posts.Consumers;

public class DeleteBucketConsumer(IS3StorageService s3StorageService) : IConsumer<DeleteBucketMessage>
{
	public async Task Consume(ConsumeContext<DeleteBucketMessage> context)
	{
		if (await s3StorageService.BucketExistAsync(context.Message.BucketIdentifier))
			await s3StorageService.RemoveBucketAsync(context.Message.BucketIdentifier);
	}
}