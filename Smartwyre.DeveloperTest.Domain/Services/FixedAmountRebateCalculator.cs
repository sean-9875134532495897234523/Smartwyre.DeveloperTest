using Smartwyre.DeveloperTest.Domain.Entities;
using Smartwyre.DeveloperTest.Domain.Entities.RebateCalculations;

namespace Smartwyre.DeveloperTest.Domain.Services;

public class FixedAmountRebateCalculator : RebateCalculatorBase
{
    public override bool Supports(RebateCalculationParameters parameters)
    {
        return parameters.Rebate != null &&
            parameters.Rebate.Amount != 0;
    }

    protected override RebateCalculation CalculateInternal(RebateCalculationParameters parameters)
    {
        return new FixedAmountRebateCalculation()
        {
            RebateIdentifier = parameters.Rebate.Identifier,
            ProductIdentifier = parameters.Product?.Identifier,
            Amount = parameters.Rebate.Amount,
        };
    }
}
