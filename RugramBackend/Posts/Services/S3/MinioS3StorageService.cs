using System.Reactive.Linq;
using Minio;
using Minio.DataModel.Args;

namespace Posts.Services.S3;

public class MinioS3StorageService(IMinioClient minioClient) : IS3StorageService
{
	public async Task<bool> BucketExistAsync(Guid bucketIdentifier)
	{
		var args = new BucketExistsArgs()
			.WithBucket(bucketIdentifier.ToString());

		return await minioClient.BucketExistsAsync(args);
	}

	public async Task CreateBucketAsync(Guid bucketIdentifier)
	{
		var args = new MakeBucketArgs()
			.WithBucket(bucketIdentifier.ToString());

		await minioClient.MakeBucketAsync(args);
	}

	public async Task RemoveBucketAsync(Guid bucketIdentifier)
	{
		var listObjectsArgs = new ListObjectsArgs()
			.WithBucket(bucketIdentifier.ToString());

		var objectNames = await minioClient
			.ListObjectsAsync(listObjectsArgs)
			.Select(x => x.Key)
			.ToList();

		if (objectNames.Any())
		{
			var removeObjectsArgs = new RemoveObjectsArgs()
				.WithBucket(bucketIdentifier.ToString())
				.WithObjects(objectNames);

			await minioClient.RemoveObjectsAsync(removeObjectsArgs);
		}

		var removeBucketArgs = new RemoveBucketArgs()
			.WithBucket(bucketIdentifier.ToString());

		await minioClient.RemoveBucketAsync(removeBucketArgs);
	}

	public async Task PutFileInBucketAsync(Stream fileStream, Guid fileIdentifier, Guid bucketIdentifier)
	{
		var args = new PutObjectArgs()
			.WithBucket(bucketIdentifier.ToString())
			.WithStreamData(fileStream)
			.WithObject(fileIdentifier.ToString())
			.WithObjectSize(fileStream.Length);

		await minioClient.PutObjectAsync(args);
	}

	public async Task RemoveFileFromBucketAsync(Guid fileIdentifier, Guid bucketIdentifier)
	{
		var args = new RemoveObjectArgs()
			.WithBucket(bucketIdentifier.ToString())
			.WithObject(fileIdentifier.ToString());

		await minioClient.RemoveObjectAsync(args);
	}

	public async Task<MemoryStream> GetFileFromBucketAsync(Guid fileIdentifier, Guid bucketIdentifier)
	{
		var response = new MemoryStream();
		var args = new GetObjectArgs()
			.WithBucket(bucketIdentifier.ToString())
			.WithObject(fileIdentifier.ToString())
			.WithCallbackStream(stream => stream.CopyTo(response));

		await minioClient.GetObjectAsync(args);

		response.Position = 0;
		return response;
	}
}