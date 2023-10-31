using MediatR;

namespace Contracts;

public interface IGrpcRequestHandler<in TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
    where TRequest : IGrpcRequest<TResponse> where TResponse : IGrpcResponse
{
}