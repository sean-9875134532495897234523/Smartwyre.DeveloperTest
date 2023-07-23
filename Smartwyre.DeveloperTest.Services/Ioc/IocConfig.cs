using Microsoft.Extensions.DependencyInjection;

namespace Smartwyre.DeveloperTest.Services.Ioc;

public static class IocConfig
{
    public static void RegisterTypes(IServiceCollection services)
    {
        services.AddScoped<IRebateService, RebateService>();
    }
}
