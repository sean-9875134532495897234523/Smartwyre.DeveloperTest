using Smartwyre.DeveloperTest.Domain.Enums;

namespace Smartwyre.DeveloperTest.Data.DbModels;

public class RebateCalculationDb : DbModelBase
{
    // RebateId and ProductId Would have fk constraint
    public string? RebateId { get; set; }
    public string? ProductId { get; set; }
    public IncentiveType IncentiveType { get; set; }
    public decimal Amount { get; set; }
    public decimal Volume { get; set; }

    // If rebate amount, rebate percentage and product price can change then record what they are at the time
    public decimal RebateAmount { get; set; }
    public decimal ProductPrice { get; set; }
    public decimal RebatePercentage { get; set; }
}
