using Infrastructure.MediatR.Contracts;
using Minio.Exceptions;
using Posts.Services.S3;

namespace Posts.Features.GetPhoto;

public class GetPhotoRequestHandler(IS3StorageService s3StorageService)
	: IGrpcRequestHandler<GetPhotoRequest, GetPhotoResponse>
{
	public async Task<GrpcResult<GetPhotoResponse>> Handle(
		GetPhotoRequest request,
		CancellationToken cancellationToken)
	{
		MemoryStream fileStream;

		try
		{
			fileStream = await s3StorageService.GetFileFromBucketAsync(
				request.PhotoId,
				request.ProfileId);
		}
		catch (BucketNotFoundException)
		{
			return StatusCodes.Status404NotFound;
		}
		catch (FileNotFoundException)
		{
			return StatusCodes.Status404NotFound;
		}
		
		return new GetPhotoResponse(fileStream.ToArray());
	}
}