using AutoMapper;
using Grpc.Core;
using MediatR;

namespace Auth.Features.SendEmailConfirmation;

public class SendEmailConfirmationGrpcService : AuthMicroservice.AuthMicroserviceBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public SendEmailConfirmationGrpcService(IMediator mediator, IMapper mapper)
    {
        _mapper = mapper;
        _mediator = mediator;
    }

    public override async Task<SendEmailConfirmationGrpcResponse> SendEmailConfirmation(
        SendEmailConfirmationGrpcRequest request, ServerCallContext context)
    {
        return _mapper.Map<SendEmailConfirmationGrpcResponse>(
            await _mediator.Send(_mapper.Map<SendEmailConfirmationRequest>(request)));
    }
}