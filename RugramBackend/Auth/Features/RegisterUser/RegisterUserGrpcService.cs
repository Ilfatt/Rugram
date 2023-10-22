using AutoMapper;
using Grpc.Core;
using MediatR;

namespace Auth.Features.RegisterUser;

public class RegisterUserGrpcService : AuthMicroservice.AuthMicroserviceBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public RegisterUserGrpcService(IMediator mediator,IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }
    public override async Task<RegisterUserGrpcResponse> RegisterUser(RegisterUserGrpcRequest request, ServerCallContext context)
    {
        return _mapper.Map<RegisterUserGrpcResponse>(
            await _mediator.Send(_mapper.Map<RegisterUserRequest>(request)));
    }
}