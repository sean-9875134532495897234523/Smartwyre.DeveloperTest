using Smartwyre.DeveloperTest.Domain.Entities;
using Smartwyre.DeveloperTest.Domain.Entities.RebateCalculations;
using Smartwyre.DeveloperTest.Domain.Exceptions;

namespace Smartwyre.DeveloperTest.Domain.Services;

public abstract class RebateCalculatorBase : IRebateCalculator
{
    public RebateCalculation Calculate(RebateCalculationParameters parameters)
    {
        if (!Supports(parameters))
        {
            throw new UnsupportedRebateCalculationException();
        }

        return CalculateInternal(parameters);
    }

    public abstract bool Supports(RebateCalculationParameters parameters);

    protected abstract RebateCalculation CalculateInternal(RebateCalculationParameters parameters);
}
