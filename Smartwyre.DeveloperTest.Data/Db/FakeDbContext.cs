using Smartwyre.DeveloperTest.Data.DbModels;
using Smartwyre.DeveloperTest.Domain.Enums;

namespace Smartwyre.DeveloperTest.Data.Db;

internal class FakeDbContext : IDbContext
{
    public IList<RebateDb> Rebates { get; }

    public IList<ProductDb> Products { get; }

    public IList<RebateCalculationDb> RebateCalculations { get; }

    public FakeDbContext()
    {
        Rebates = new List<RebateDb>();
        Products = new List<ProductDb>();
        RebateCalculations = new List<RebateCalculationDb>();

        AddInitialData();
    }

    private void AddInitialData()
    {
        Rebates.Add(new RebateDb() { Id = Guid.NewGuid().ToString(), Identifier = "rebate-001", Amount = 10, Percentage = 0, Incentive = IncentiveType.FixedCashAmount });
        Rebates.Add(new RebateDb() { Id = Guid.NewGuid().ToString(), Identifier = "rebate-002", Amount = 0, Percentage = 10, Incentive = IncentiveType.FixedRateRebate });
        Rebates.Add(new RebateDb() { Id = Guid.NewGuid().ToString(), Identifier = "rebate-003", Amount = 10, Percentage = 10, Incentive = IncentiveType.AmountPerUom });

        Products.Add(new ProductDb() { Id = Guid.NewGuid().ToString(), Identifier = "product-001", Price = 10, Uom = "uom", SupportedIncentives = new List<IncentiveType>() { IncentiveType.FixedCashAmount } });
        Products.Add(new ProductDb() { Id = Guid.NewGuid().ToString(), Identifier = "product-002", Price = 10, Uom = "uom", SupportedIncentives = new List<IncentiveType>() { IncentiveType.FixedRateRebate } });
        Products.Add(new ProductDb() { Id = Guid.NewGuid().ToString(), Identifier = "product-003", Price = 10, Uom = "uom", SupportedIncentives = new List<IncentiveType>() { IncentiveType.AmountPerUom } });
    }
}
