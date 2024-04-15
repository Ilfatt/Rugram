using Gateway.Endpoints.AuthMicroservice.Login;
using Gateway.Endpoints.AuthMicroservice.RegisterUser;
using Gateway.Endpoints.AuthMicroservice.SendEmailConfirmation;
using Gateway.Endpoints.AuthMicroservice.UpdateJwtToken;
using Gateway.Endpoints.PostsMicroservice.GetPhoto;
using Gateway.Endpoints.PostsMicroservice.GetPosts;
using Infrastructure.AutoMapper;

namespace Gateway.AutoMapper;

public class MapperProfile : BaseMappingProfile
{
	public MapperProfile()
	{
		CreateMap<RegisterUserRequest, RegisterUserGrpcRequest>();
		CreateMap<RegisterUserGrpcResponse, RegisterUserResponse>();

		CreateMap<SendEmailConfirmationRequest, SendEmailConfirmationGrpcRequest>();

		CreateMap<LoginRequest, LoginGrpcRequest>();
		CreateMap<LoginGrpcResponse, LoginResponse>();

		CreateMap<UpdateJwtTokenRequest, UpdateJwtTokenGrpcRequest>();
		CreateMap<UpdateJwtTokenGrpcResponse, UpdateJwtTokenResponse>();

		CreateMap<PostGrpc, PostDto>();
		CreateMap<GetPostsGrpcResponse, GetPostsResponse>();

		CreateMap<GetPhotoGrpcResponse, GetPhotoResponse>();
	}
}