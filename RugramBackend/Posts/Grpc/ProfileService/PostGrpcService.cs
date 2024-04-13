using AutoMapper;
using Grpc.Core;
using MediatR;
using Posts.Features;

namespace Posts.Grpc.ProfileService;

public class PostGrpcService(IMediator mediator, IMapper mapper) : PostMicroservice.PostMicroserviceBase
{
	public override async Task<GetPhotoGrpcResponse> GetPhoto(
		GetPhotoGrpcRequest request,
		ServerCallContext context)
	{
		return mapper.Map<GetPhotoGrpcResponse>(
			await mediator.Send(mapper.Map<GetPhotoRequest>(request)));
	}
}