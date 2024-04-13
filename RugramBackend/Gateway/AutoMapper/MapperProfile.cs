using AutoMapper;
using Gateway.Endpoints.AuthMicroservice.Login;
using Gateway.Endpoints.AuthMicroservice.RegisterUser;
using Gateway.Endpoints.AuthMicroservice.SendEmailConfirmation;
using Gateway.Endpoints.AuthMicroservice.UpdateJwtToken;
using Gateway.Endpoints.PostsMicroservice.GetPhoto;
using Google.Protobuf;

namespace Gateway.AutoMapper;

public class MapperProfile : Profile
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

		CreateMap<GetPhotoGrpcResponse, GetPhotoResponse>()
			.ForMember(x => x.Photo, x =>
				x.MapFrom(response => response.Photo.ToArray()));
	}
}