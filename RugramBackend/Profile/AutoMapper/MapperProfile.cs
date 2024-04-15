using Infrastructure.AutoMapper;
using Profile.Features.CreateProfile;
using Profile.Features.Subscribe;
using Profile.Features.Unsubscribe;

namespace Profile.AutoMapper;

public class MapperProfile : BaseMappingProfile
{
	public MapperProfile()
	{
		CreateMap<CreateProfileGrpcRequest, CreateProfileRequest>();
		CreateMapFromResult<CreateProfileGrpcResponse>();

		CreateMap<SubscribeGrpcRequest, SubscribeRequest>();
		CreateMapFromResult<SubscribeGrpcResponse>();

		CreateMap<UnsubscribeGrpcRequest, UnsubscribeRequest>();
		CreateMapFromResult<UnsubscribeGrpcResponse>();
	}
}