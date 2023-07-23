using Smartwyre.DeveloperTest.Domain.Enums;
using Smartwyre.DeveloperTest.Domain.Services;

namespace Smartwyre.DeveloperTest.Domain.Factories;

public interface IRebateCalculatorFactory
{
    IRebateCalculator GetInstance(IncentiveType incentiveType);
}
