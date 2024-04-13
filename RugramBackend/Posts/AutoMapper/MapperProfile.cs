using AutoMapper;
using Google.Protobuf;
using Infrastructure.MediatR.Contracts;
using Posts.Features;

namespace Posts.AutoMapper;

public class MapperProfile : Profile
{
	public MapperProfile()
	{
		CreateMap<GetPhotoGrpcRequest, GetPhotoRequest>()
			.ForMember(x => x.ProfileId, x =>
				x.MapFrom(request => new Guid(request.ProfileId)))
			.ForMember(x => x.PhotoId, x =>
				x.MapFrom(request => new Guid(request.PhotoId)));
		CreateMap<GrpcResult<GetPhotoResponse>, GetPhotoGrpcResponse>()
			.ForMember(x => x.Photo,
				x => x.MapFrom(result => 
					result.Body != null ? ByteString.CopyFrom(result.Body.Photo) : ByteString.Empty));
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

	private void CreateMapFromResult<TDestination>() => CreateMap<GrpcResult, TDestination>();
}