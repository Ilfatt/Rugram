using Auth.Features.RegisterUser;
using Auth.Features.SendEmailConfirmation;
using AutoMapper;
using Contracts;

namespace Auth.AutoMapper;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<RegisterUserGrpcRequest, RegisterUserRequest>();
        CreateMap<GrpcResult<RegisterUserResponse>, RegisterUserGrpcResponse>()
            .AfterMap((src, dest, context) =>
            {
                if (src.Body != null) context.Mapper.Map(src.Body, dest);
            });

        CreateMap<SendEmailConfirmationGrpcRequest, SendEmailConfirmationRequest>();
        CreateMap<GrpcResult<SendEmailConfirmationResponse>, SendEmailConfirmationGrpcResponse>()
            .AfterMap((src, dest, context) =>
            {
                if (src.Body != null) context.Mapper.Map(src.Body, dest);
            });
    }
}