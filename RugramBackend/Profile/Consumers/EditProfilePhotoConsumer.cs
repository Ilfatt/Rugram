using Contracts.RabbitMq;
using Infrastructure.S3;
using MassTransit;
using Minio.Exceptions;

namespace Profile.Consumers;

public class EditProfilePhotoConsumer(IS3StorageService s3StorageService)
	: IConsumer<EditProfilePhotoMessage>
{
	public async Task Consume(ConsumeContext<EditProfilePhotoMessage> context)
	{
		try
		{
			await s3StorageService.RemoveFileFromBucketAsync(
				context.Message.ProfileId,
				context.Message.ProfileId,
				context.CancellationToken);
		}
		catch (BucketNotFoundException)
		{
			await s3StorageService.CreateBucketAsync(context.Message.ProfileId, context.CancellationToken);
			throw;
		}

		await s3StorageService.PutFileInBucketAsync(
			new MemoryStream(context.Message.Photo),
			context.Message.ProfileId,
			context.Message.ProfileId,
			context.CancellationToken);
	}
}