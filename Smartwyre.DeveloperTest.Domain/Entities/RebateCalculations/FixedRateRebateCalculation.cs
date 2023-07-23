namespace Smartwyre.DeveloperTest.Domain.Entities;

public class FixedRateRebateCalculation : VolumeBasedRebateCalculation
{
    public decimal ProductPrice { get; set; }
    public decimal RebatePercentage { get; set; }
}
