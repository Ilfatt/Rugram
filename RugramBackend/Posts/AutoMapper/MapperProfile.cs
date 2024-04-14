using Infrastructure.AutoMapper;
using Posts.Features.GetPhoto;
using Posts.Features.GetPosts;

namespace Posts.AutoMapper;

public class MapperProfile : BaseMappingProfile
{
	public MapperProfile()
	{
		CreateMap<GetPhotoGrpcRequest, GetPhotoRequest>();
		CreateMapFromResult<GetPhotoResponse, GetPhotoGrpcResponse>();

		CreateMap<GetPostsGrpcRequest, GetPostsRequest>();

		CreateMap<PostDto, PostGrpc>();
		CreateMapFromResult<GetPostsResponse, GetPostsGrpcResponse>();
	}
}