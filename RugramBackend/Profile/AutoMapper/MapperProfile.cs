using Infrastructure.MediatR.Contracts;
using Profile.Features.CreateProfile;

namespace Profile.AutoMapper;

public class MapperProfile : global::AutoMapper.Profile
{
	public MapperProfile()
	{
		CreateMap<CreateProfileGrpcRequest, CreateProfileRequest>()
			.ForMember(x => x.ProfileId, x => 
				x.MapFrom(request => new Guid(request.ProfileId)));
		CreateMapFromResult<CreateProfileResponse, CreateProfileGrpcResponse>();
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