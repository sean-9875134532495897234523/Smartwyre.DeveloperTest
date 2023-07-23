using Smartwyre.DeveloperTest.Domain.Enums;
using Smartwyre.DeveloperTest.Domain.Services;

namespace Smartwyre.DeveloperTest.Domain.Factories;

public class RebateCalculatorFactory : IRebateCalculatorFactory
{
    public IRebateCalculator GetInstance(IncentiveType incentiveType)
    {
        return incentiveType switch
        {
            IncentiveType.FixedCashAmount => new FixedAmountRebateCalculator(),
            IncentiveType.FixedRateRebate => new FixedRateRebateCalculator(),
            IncentiveType.AmountPerUom => new AmountPerUomRebateCalculator(),
            _ => throw new ArgumentOutOfRangeException(),
        };
    }
}
