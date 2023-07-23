using Microsoft.Extensions.DependencyInjection;
using Smartwyre.DeveloperTest.Data.Db;
using Smartwyre.DeveloperTest.Data.Stores;
using Smartwyre.DeveloperTest.Domain.Entities;
using Smartwyre.DeveloperTest.Domain.Stores;

namespace Smartwyre.DeveloperTest.Data.IOC;

public static class IocConfig
{
    public static void RegisterTypes(IServiceCollection services)
    {
        services.AddSingleton<IDbContext, FakeDbContext>();
        services.AddScoped<IStore<Rebate>, RebateDataStore>();
        services.AddScoped<IStore<Product>, ProductDataStore>();
        services.AddScoped<IStore<RebateCalculation>, RebateCalculationStore>();
    }
}
