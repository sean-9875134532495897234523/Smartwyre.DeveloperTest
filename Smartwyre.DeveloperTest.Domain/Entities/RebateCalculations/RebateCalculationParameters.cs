namespace Smartwyre.DeveloperTest.Domain.Entities.RebateCalculations;

public class RebateCalculationParameters
{
    public Rebate Rebate { get; }
    public Product Product { get; }
    public decimal Volume { get; }
    public RebateCalculationParameters(Rebate rebate, Product product, decimal volume)
    {
        Rebate = rebate;
        Product = product;
        Volume = volume;
    }
}
