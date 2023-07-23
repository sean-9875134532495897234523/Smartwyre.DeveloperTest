namespace Smartwyre.DeveloperTest.Domain.Entities;

public abstract class RebateCalculation : IDomainEntity
{
    public string? Identifier { get; set; }
    public string? RebateIdentifier { get; set; }
    public string? ProductIdentifier { get; set; }
    public decimal Amount { get; set; }
}
