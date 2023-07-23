using Smartwyre.DeveloperTest.Domain.Enums;

namespace Smartwyre.DeveloperTest.Data.DbModels;

public class ProductDb : DbModelBase
{
    public decimal Price { get; set; }
    public string? Uom { get; set; }
    public IEnumerable<IncentiveType>? SupportedIncentives { get; set; }
}
