using MediatR;

namespace Contracts;

public interface IGrpcRequestHandler<in TRequest, TResponse> : IRequestHandler<TRequest, GrpcResult<TResponse>>
    where TRequest : IGrpcRequest<GrpcResult<TResponse>>
{
}