using System.Reflection;
using eScout_v2.Application.UseCases.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace eScout_v2.Application.UseCases.Extensions;

public static class UseCaseExtensions
{
    public static IServiceCollection AddUseCases(this IServiceCollection services, Assembly? assemblyToScan = null)
    {
        Assembly targetAssembly = assemblyToScan ?? Assembly.GetEntryAssembly()!;
        services.RegisterUseCasesFromAssembly(targetAssembly);

        return services;
    }
    
    private static IServiceCollection RegisterUseCasesFromAssembly(
        this IServiceCollection services,
        Assembly targetAssembly)
    {
        IEnumerable<Type> useCaseTypes = targetAssembly.GetTypes()
            .Where(type => !type.IsAbstract && IsConcreteUseCase(type));

        foreach (Type useCaseType in useCaseTypes)
        {
            foreach (Type closedContract in GetUseCaseContracts(useCaseType))
            {
                services.AddScoped(closedContract, useCaseType);
            }
        }

        return services;
    }

    private static bool IsConcreteUseCase(Type type)
    {
        return GetUseCaseContracts(type).Any();
    }

    private static IEnumerable<Type> GetUseCaseContracts(Type type)
    {
        return type.GetInterfaces().Where(IsUseCaseContract);
    }

    private static bool IsUseCaseContract(Type interfaceType)
    {
        if (interfaceType == typeof(IUseCaseVoid))
            return true;

        if (!interfaceType.IsGenericType)
            return false;

        Type definition = interfaceType.GetGenericTypeDefinition();

        return definition == typeof(IUseCase<,>)
            || definition == typeof(IUseCase<>)
            || definition == typeof(IUseCaseVoid<>);
    }
}