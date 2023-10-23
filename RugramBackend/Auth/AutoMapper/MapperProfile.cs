using Auth.Features.RegisterUser;
using Auth.Features.SendEmailConfirmation;
using AutoMapper;

namespace Auth.AutoMapper;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<RegisterUserGrpcRequest, RegisterUserRequest>();
        CreateMap<RegisterUserGrpcResponse, RegisterUserResponse>();
        
        CreateMap<SendEmailConfirmationGrpcRequest, SendEmailConfirmationRequest>();
        CreateMap<SendEmailConfirmationGrpcResponse, SendEmailConfirmationResponse>();
    }
}