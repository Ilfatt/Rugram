using AutoMapper;
using Grpc.Core;
using MediatR;
using Profile.Features.CreateProfile;

namespace Profile.Grpc.ProfileForAuthService;

public class ProfileForAuthGrpcService(IMapper mapper, IMediator mediator) 
	: ProfileMicroservice.ProfileMicroserviceBase
{
	public override async Task<CreateProfileGrpcResponse> CreateProfile(
		CreateProfileGrpcRequest request,
		ServerCallContext context)
	{
		return mapper.Map<CreateProfileGrpcResponse>(
			await mediator.Send(mapper.Map<CreateProfileRequest>(request)));
	}
}