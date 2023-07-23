using Smartwyre.DeveloperTest.Domain.Entities;
using Smartwyre.DeveloperTest.Domain.Entities.RebateCalculations;

namespace Smartwyre.DeveloperTest.Domain.Services;

public class FixedRateRebateCalculator : RebateCalculatorBase
{
    public override bool Supports(RebateCalculationParameters parameters)
    {
        return parameters.Rebate != null &&
            parameters.Product != null &&
            parameters.Rebate.Percentage != 0 &&
            parameters.Product.Price != 0 &&
            parameters.Volume != 0;
    }

    protected override RebateCalculation CalculateInternal(RebateCalculationParameters parameters)
    {
        decimal amount = parameters.Product.Price * parameters.Rebate.Percentage * parameters.Volume;

        return new FixedRateRebateCalculation()
        {
            RebateIdentifier = parameters.Rebate.Identifier,
            ProductIdentifier = parameters.Product.Identifier,
            Amount = amount,
            ProductPrice = parameters.Product.Price,
            Volume = parameters.Volume,
        };
    }
}
