using AutoMapper;
using Gateway.Endpoints.AuthMicroservice.Login;
using Gateway.Endpoints.AuthMicroservice.RegisterUser;
using Gateway.Endpoints.AuthMicroservice.SendEmailConfirmation;
using Gateway.Endpoints.AuthMicroservice.UpdateJwtToken;
using Gateway.Endpoints.ProfileMicroservice.Subscribe;
using Gateway.Endpoints.ProfileMicroservice.Unsubscribe;

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
	}
}