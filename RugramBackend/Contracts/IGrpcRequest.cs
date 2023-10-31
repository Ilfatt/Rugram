using MediatR;

namespace Contracts;

public interface IGrpcRequest<TResponse> : IRequest<TResponse>
{
}