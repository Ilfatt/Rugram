using System.Reflection;
using FluentValidation;
using Infrastructure.MediatR.Behaviors;
using Infrastructure.MediatR.Contracts;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.MediatR.Extensions;

public static class ServiceCollectionExtensions
{
    private static readonly Type RequestType = typeof(IGrpcRequest<>);
    private static readonly Type PipelineBehaviorType = typeof(IPipelineBehavior<,>);
    private static readonly Type ResultType = typeof(GrpcResult<>);

    /// <summary>
    /// Добавление бехейворов возвращающих GrpcResult в IServiceCollection
    /// </summary>
    /// <param name="serviceCollection">IServiceCollection</param>
    /// <param name="assembly">Assembly</param>
    /// <param name="behaviorType">реализация бехейвора</param>
    /// <param name="constraintType">Маркерный интерфейс</param>
    public static void AddBehaviorsReturningGrpcResultFromAssembly(
        this IServiceCollection serviceCollection,
        Assembly assembly,
        Type behaviorType,
        Type? constraintType = null)
    {
        var requests = assembly.GetTypes()
            .Where(type => constraintType == null || type.IsAssignableTo(constraintType))
            .Where(type => IsAssignableToGenericType(type, RequestType) &&
                           type is { IsClass: true, IsAbstract: false })
            .ToList();

        var responses = requests
            .Select(request => request.GetInterface(RequestType.Name)!.GetGenericArguments()[0])
            .ToList();

        for (var i = 0; i < requests.Count; i++)
        {
            serviceCollection.AddTransient(
                PipelineBehaviorType.MakeGenericType(requests[i], ResultType.MakeGenericType(responses[i])),
                behaviorType.MakeGenericType(requests[i], responses[i]));
        }
    }

    /// <summary>
    /// Добавление ValidationBehavior для всех запросов в сброке имеющих Validator : AbstractValidator
    /// в IServiceCollection
    /// </summary>
    /// <param name="serviceCollection">IServiceCollection</param>
    /// <param name="assembly">Assembly</param>
    public static void AddValidationBehaviorsFromAssembly(
        this IServiceCollection serviceCollection,
        Assembly assembly)
    {
        var requests = assembly.GetTypes()
            .Where(type => IsAssignableToGenericType(type, typeof(IValidator<>)) &&
                           type is { IsClass: true, IsAbstract: false })
            .Select(type => type.BaseType!.GetGenericArguments()[0])
            .ToList();

        var responses = requests
            .Select(request => request.GetInterface(RequestType.Name)!.GetGenericArguments()[0])
            .ToList();

        for (var i = 0; i < requests.Count; i++)
        {
            serviceCollection.AddTransient(
                PipelineBehaviorType.MakeGenericType(requests[i], ResultType.MakeGenericType(responses[i])),
                typeof(ValidationBehavior<,>).MakeGenericType(requests[i], responses[i]));
        }
    }

    private static bool IsAssignableToGenericType(Type givenType, Type genericType)
    {
        var interfaceTypes = givenType.GetInterfaces();

        if (interfaceTypes.Any(type => type.IsGenericType && type.GetGenericTypeDefinition() == genericType))
        {
            return true;
        }

        if (givenType.IsGenericType && givenType.GetGenericTypeDefinition() == genericType)
        {
            return true;
        }

        var baseType = givenType.BaseType;

        return baseType != null && IsAssignableToGenericType(baseType, genericType);
    }
}