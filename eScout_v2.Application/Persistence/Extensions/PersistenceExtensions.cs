using System.Reflection;
using eScout_v2.Application.Persistence.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace eScout_v2.Application.Persistence.Extensions;

public static class PersistenceExtensions
{
    public static IServiceCollection AddPersistence<TDbContext>(this IServiceCollection services, bool overrideDefaultRepositoryAssembly = false) where TDbContext : DbContext
    {
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

        if (!overrideDefaultRepositoryAssembly)
        {
            services.RegisterRepositoriesFromAssembly();
        }
        
        return services;
    }

    private static IServiceCollection RegisterRepositoriesFromAssembly(
        this IServiceCollection services,
        Assembly? assemblyToScan = null)
    {
        Assembly targetAssembly = assemblyToScan ?? Assembly.GetEntryAssembly()!;

        IEnumerable<Type> repositoryTypes = targetAssembly.GetTypes()
            .Where(type => !type.IsAbstract && !type.IsInterface && IsConcreteRepository(type));

        foreach (Type repositoryType in repositoryTypes)
        {
            foreach (Type repositoryContract in GetRepositoryContracts(repositoryType))
            {
                services.AddScoped(repositoryContract, repositoryType);
            }
        }

        return services;
    }

    private static bool IsConcreteRepository(Type type)
    {
        return GetRepositoryContracts(type).Any();
    }

    private static IEnumerable<Type> GetRepositoryContracts(Type type)
    {
        return type.GetInterfaces().Where(IsRepositoryContract);
    }

    private static bool IsRepositoryContract(Type interfaceType)
    {
        if (!interfaceType.IsInterface || !interfaceType.IsGenericType)
            return false;

        return interfaceType.GetGenericTypeDefinition() == typeof(IRepository<>);
    }
}