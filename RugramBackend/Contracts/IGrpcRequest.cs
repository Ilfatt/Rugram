using MediatR;

namespace Contracts;

public interface IGrpcRequest<out TResponse> : IRequest<TResponse> where TResponse : IGrpcResponse
{
}