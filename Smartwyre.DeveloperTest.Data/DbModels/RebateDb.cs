using Smartwyre.DeveloperTest.Domain.Enums;

namespace Smartwyre.DeveloperTest.Data.DbModels;

public class RebateDb : DbModelBase
{
    public IncentiveType Incentive { get; set; }
    public decimal Amount { get; set; }
    public decimal Percentage { get; set; }
}
