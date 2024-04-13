using Infrastructure.MediatR.Contracts;
using Profile.Features.CreateProfile;
using Profile.Features.Subscribe;
using Profile.Features.Unsubscribe;

namespace Profile.AutoMapper;

public class MapperProfile : global::AutoMapper.Profile
{
	public MapperProfile()
	{
		CreateMap<CreateProfileGrpcRequest, CreateProfileRequest>()
			.ForMember(x => x.ProfileId, x =>
				x.MapFrom(request => new Guid(request.ProfileId)));
		CreateMapFromResult<CreateProfileResponse, CreateProfileGrpcResponse>();

		CreateMap<SubscribeGrpcRequest, SubscribeRequest>();
		CreateMapFromResult<SubscribeResponse, SubscribeGrpcResponse>();

		CreateMap<UnsubscribeGrpcRequest, UnsubscribeRequest>();
		CreateMapFromResult<UnsubscribeResponse, UnsubscribeGrpcResponse>();
	}

	private void CreateMapFromResult<TSource, TDestination>()
	{
		CreateMap<TSource, TDestination>();
		CreateMap<GrpcResult<TSource>, TDestination>()
			.AfterMap((src, dest, context) =>
			{
				if (src.Body != null) context.Mapper.Map(src.Body, dest);
			});
	}
}