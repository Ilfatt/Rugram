using Auth.Features.RegisterUser;
using AutoMapper;

namespace Auth.AutoMapper;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<RegisterUserGrpcRequest, RegisterUserRequest>();
        CreateMap<RegisterUserResponse, RegisterUserGrpcResponse>();
    }
}