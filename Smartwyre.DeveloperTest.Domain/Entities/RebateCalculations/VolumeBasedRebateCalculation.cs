namespace Smartwyre.DeveloperTest.Domain.Entities;

public abstract class VolumeBasedRebateCalculation : RebateCalculation
{
    public decimal Volume { get; set; }
}
