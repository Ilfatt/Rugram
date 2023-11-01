using MediatR;

namespace Infrastructure.MediatR.Contracts;

public interface IGrpcRequest<TResponse> : IRequest<GrpcResult<TResponse>>
{
}