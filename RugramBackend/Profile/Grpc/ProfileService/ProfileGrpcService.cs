using AutoMapper;
using Grpc.Core;
using MediatR;
using Profile.Features.Subscribe;
using Profile.Features.Unsubscribe;

namespace Profile.Grpc.ProfileService;

public class ProfileGrpcService(IMapper mapper, IMediator mediator)
	: ProfileMicroservice.ProfileMicroserviceBase
{
	public override async Task<SubscribeGrpcResponse> Subscribe(
		SubscribeGrpcRequest request,
		ServerCallContext context)
	{
		return mapper.Map<SubscribeGrpcResponse>(
			await mediator.Send(mapper.Map<SubscribeRequest>(request), context.CancellationToken));
	}

	public override async Task<UnsubscribeGrpcResponse> Unsubscribe(
		UnsubscribeGrpcRequest request,
		ServerCallContext context)
	{
		return mapper.Map<UnsubscribeGrpcResponse>(
			await mediator.Send(mapper.Map<UnsubscribeRequest>(request), context.CancellationToken));
	}
}