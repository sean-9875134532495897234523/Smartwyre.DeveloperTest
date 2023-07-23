using Smartwyre.DeveloperTest.Domain.Entities;
using Smartwyre.DeveloperTest.Domain.Entities.RebateCalculations;

namespace Smartwyre.DeveloperTest.Domain.Services;

public class AmountPerUomRebateCalculator : RebateCalculatorBase
{
    public override bool Supports(RebateCalculationParameters parameters)
    {
        return parameters.Rebate != null &&
            parameters.Rebate.Amount != 0 &&
            parameters.Volume != 0;
    }

    protected override RebateCalculation CalculateInternal(RebateCalculationParameters parameters)
    {
        decimal amount = parameters.Rebate.Amount * parameters.Volume;

        return new AmountPerUomRebateCalculation()
        {
            RebateIdentifier = parameters.Rebate.Identifier,
            ProductIdentifier = parameters.Product.Identifier,
            Amount = amount,
            Volume = parameters.Volume,
        };
    }
}
