using Smartwyre.DeveloperTest.Data.DbModels;

namespace Smartwyre.DeveloperTest.Data.Db;

public interface IDbContext
{
    public IList<RebateDb> Rebates { get; }
    public IList<ProductDb> Products { get; }
    public IList<RebateCalculationDb> RebateCalculations { get; }
}
