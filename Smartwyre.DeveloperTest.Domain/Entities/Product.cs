using Smartwyre.DeveloperTest.Domain.Enums;

namespace Smartwyre.DeveloperTest.Domain.Entities;

public class Product : IDomainEntity
{
    public string? Identifier { get; set; }
    public decimal Price { get; set; }
    public string? Uom { get; set; }
    public IEnumerable<IncentiveType>? SupportedIncentives { get; set; }

    public bool Supports(Rebate rebate)
    {
        return SupportedIncentives != null &&
            SupportedIncentives.Contains(rebate.Incentive);
    }
}
