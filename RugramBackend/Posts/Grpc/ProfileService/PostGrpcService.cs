using AutoMapper;
using Grpc.Core;
using MediatR;
using Posts.Features.GetPhoto;
using Posts.Features.GetPosts;

namespace Posts.Grpc.ProfileService;

public class PostGrpcService(IMediator mediator, IMapper mapper) : PostMicroservice.PostMicroserviceBase
{
	public override async Task<GetPhotoGrpcResponse> GetPhoto(
		GetPhotoGrpcRequest request,
		ServerCallContext context)
	{
		return mapper.Map<GetPhotoGrpcResponse>(
			await mediator.Send(mapper.Map<GetPhotoRequest>(request), context.CancellationToken));
	}

	public override async Task<GetPostsGrpcResponse> GetPosts(
		GetPostsGrpcRequest request,
		ServerCallContext context)
	{
		return mapper.Map<GetPostsGrpcResponse>(
			await mediator.Send(mapper.Map<GetPostsRequest>(request), context.CancellationToken));
	}
}