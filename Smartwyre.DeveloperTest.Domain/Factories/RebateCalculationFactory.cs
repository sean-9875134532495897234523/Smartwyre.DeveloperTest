using Smartwyre.DeveloperTest.Domain.Entities;
using Smartwyre.DeveloperTest.Domain.Enums;

namespace Smartwyre.DeveloperTest.Domain.Factories;

public static class RebateCalculationFactory
{
    public static RebateCalculation GetInstance(IncentiveType incentiveType)
    {
        return incentiveType switch
        {
            IncentiveType.FixedCashAmount => new FixedAmountRebateCalculation(),
            IncentiveType.FixedRateRebate => new FixedRateRebateCalculation(),
            IncentiveType.AmountPerUom => new AmountPerUomRebateCalculation(),
            _ => throw new ArgumentOutOfRangeException(),
        };
    }
}
