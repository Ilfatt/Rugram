using Auth.Features.RegisterUser;
using Auth.Features.SendEmailConfirmation;
using AutoMapper;

namespace Auth.AutoMapper;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<RegisterUserGrpcRequest, RegisterUserRequest>();
        CreateMap<RegisterUserResponse, RegisterUserGrpcResponse>();

        CreateMap<SendEmailConfirmationGrpcRequest, SendEmailConfirmationRequest>();
        CreateMap<SendEmailConfirmationResponse, SendEmailConfirmationGrpcResponse>();
    }
}