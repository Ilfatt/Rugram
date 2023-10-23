using AutoMapper;
using Gateway.Endpoints.AuthMicroservice.RegisterUser;
using Gateway.Endpoints.AuthMicroservice.SendEmailConfirmation;

namespace Gateway.AutoMapper;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<RegisterUserRequest, RegisterUserGrpcRequest>();
        CreateMap<RegisterUserGrpcResponse, RegisterUserResponse>();

        CreateMap<SendEmailConfirmationRequest, SendEmailConfirmationGrpcRequest>();
    }
}