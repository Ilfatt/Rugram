using Auth.Features.Login;
using Auth.Features.RegisterUser;
using Auth.Features.SendEmailConfirmation;
using Auth.Features.UpdateJwtToken;
using AutoMapper;
using Grpc.Core;
using MediatR;

namespace Auth.Grpc;

public class AuthGrpcService : AuthMicroservice.AuthMicroserviceBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public AuthGrpcService(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    public override async Task<RegisterUserGrpcResponse> RegisterUser(
        RegisterUserGrpcRequest request,
        ServerCallContext context)
    {
        return _mapper.Map<RegisterUserGrpcResponse>(
            await _mediator.Send(_mapper.Map<RegisterUserRequest>(request)));
    }

    public override async Task<SendEmailConfirmationGrpcResponse> SendEmailConfirmation(
        SendEmailConfirmationGrpcRequest request,
        ServerCallContext context)
    {
        return _mapper.Map<SendEmailConfirmationGrpcResponse>(
            await _mediator.Send(_mapper.Map<SendEmailConfirmationRequest>(request)));
    }

    public override async Task<LoginGrpcResponse> Login(
        LoginGrpcRequest request,
        ServerCallContext context)
    {
        return _mapper.Map<LoginGrpcResponse>(
            await _mediator.Send(_mapper.Map<LoginRequest>(request)));
    }

    public override async Task<UpdateJwtTokenGrpcResponse> UpdateJwtTokenGrpc(
        UpdateJwtTokenGrpcRequest request,
        ServerCallContext context)
    {
        return _mapper.Map<UpdateJwtTokenGrpcResponse>(
            await _mediator.Send(_mapper.Map<UpdateJwtTokenRequest>(request)));
    }
}