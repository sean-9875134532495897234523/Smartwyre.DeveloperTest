using Microsoft.Extensions.DependencyInjection;
using Smartwyre.DeveloperTest.Domain.Factories;

namespace Smartwyre.DeveloperTest.Runner.ExtensionMethods;

internal static class IServiceCollectionExtensions
{
    public static IServiceCollection RegisterDomainTypes(this IServiceCollection services)
    {
        services.AddScoped<IRebateCalculatorFactory, RebateCalculatorFactory>();

        return services;
    }

    public static IServiceCollection RegisterApplicationTypes(this IServiceCollection services)
    {
        Services.Ioc.IocConfig.RegisterTypes(services);

        return services;
    }

    public static IServiceCollection RegisterInfrastructureTypes(this IServiceCollection services)
    {
        Data.IOC.IocConfig.RegisterTypes(services);

        return services;
    }
}
