using FluentValidation;
using Infrastructure.MediatR.Contracts;
using MediatR;

namespace Infrastructure.MediatR.Behaviors;

public class ValidationBehavior<TRequest, TResponse>(IValidator<TRequest> validator)
    : IPipelineBehavior<TRequest, GrpcResult<TResponse>> where TRequest : IGrpcRequest<TResponse>
{
    public async Task<GrpcResult<TResponse>> Handle(
        TRequest request,
        RequestHandlerDelegate<GrpcResult<TResponse>> next,
        CancellationToken cancellationToken)
    {
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid) return 400;

        return await next();
    }
}