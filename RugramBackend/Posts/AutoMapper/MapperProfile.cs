using Infrastructure.AutoMapper;
using PostForProfileMicroservice;
using Posts.Features.GetFeed;
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

		CreateMap<GetFeedGrpcRequest, GetFeedRequest>();
		CreateMap<ProfilePostDto, ProfilePostGrpc>();
		CreateMapFromResult<GetPostsResponse, GetPostsGrpcResponse>();

		CreateMapFromResult<GetFeedResponse, GetFeedGrpcResponse>();
	}
}