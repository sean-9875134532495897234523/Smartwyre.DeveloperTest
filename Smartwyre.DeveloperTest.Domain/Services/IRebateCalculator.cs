using Smartwyre.DeveloperTest.Domain.Entities;
using Smartwyre.DeveloperTest.Domain.Entities.RebateCalculations;

namespace Smartwyre.DeveloperTest.Domain.Services;

public interface IRebateCalculator
{
    RebateCalculation Calculate(RebateCalculationParameters parameters);

    bool Supports(RebateCalculationParameters parameters);
}
