using FluentValidation;
using Infrastructure.MediatR.Contracts;
using MediatR;

namespace Infrastructure.MediatR.Behaviors;

public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, GrpcResult<TResponse>>
    where TRequest : IGrpcRequest<TResponse>
{
    private readonly IValidator<TRequest> _validator;

    public ValidationBehavior(IValidator<TRequest> validator)
    {
        _validator = validator;
    }

    public async Task<GrpcResult<TResponse>> Handle(
        TRequest request,
        RequestHandlerDelegate<GrpcResult<TResponse>> next,
        CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid) return 400;

        return await next();
    }
}