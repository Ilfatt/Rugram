using Auth.Features.Login;
using Auth.Features.RegisterUser;
using Auth.Features.SendEmailConfirmation;
using Auth.Features.UpdateJwtToken;
using AutoMapper;
using Infrastructure.MediatR.Contracts;

namespace Auth.AutoMapper;

public class MapperProfile : Profile
{
	public MapperProfile()
	{
		CreateMap<RegisterUserGrpcRequest, RegisterUserRequest>();
		CreateMapFromResult<RegisterUserResponse, RegisterUserGrpcResponse>();

		CreateMap<SendEmailConfirmationGrpcRequest, SendEmailConfirmationRequest>();
		CreateMapFromResult<SendEmailConfirmationResponse, SendEmailConfirmationGrpcResponse>();

		CreateMap<LoginGrpcRequest, LoginRequest>();
		CreateMapFromResult<LoginResponse, LoginGrpcResponse>();

		CreateMap<UpdateJwtTokenGrpcRequest, UpdateJwtTokenRequest>();
		CreateMapFromResult<UpdateJwtTokenResponse, UpdateJwtTokenGrpcResponse>();
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