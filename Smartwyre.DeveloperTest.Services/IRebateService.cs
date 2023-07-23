using Smartwyre.DeveloperTest.Contracts;

namespace Smartwyre.DeveloperTest.Services;

public interface IRebateService
{
    Task<CalculateRebateResult> Calculate(CalculateRebateRequest request);
}
