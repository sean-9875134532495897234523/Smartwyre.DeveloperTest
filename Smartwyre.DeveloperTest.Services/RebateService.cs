using Smartwyre.DeveloperTest.Contracts;
using Smartwyre.DeveloperTest.Domain.Entities;
using Smartwyre.DeveloperTest.Domain.Entities.RebateCalculations;
using Smartwyre.DeveloperTest.Domain.Factories;
using Smartwyre.DeveloperTest.Domain.Stores;

namespace Smartwyre.DeveloperTest.Services;

public class RebateService : IRebateService
{
    private readonly IStore<Rebate> _rebateStore;
    private readonly IStore<Product> _productStore;
    private readonly IStore<RebateCalculation> _rebateCalculationStore;
    private readonly IRebateCalculatorFactory _calculatorFactory;

    public RebateService(
        IStore<Rebate> rebateStore,
        IStore<Product> productStore,
        IStore<RebateCalculation> rebateCalculationStore,
        IRebateCalculatorFactory calculatorFactory)
    {
        _rebateStore = rebateStore;
        _productStore = productStore;
        _rebateCalculationStore = rebateCalculationStore;
        _calculatorFactory = calculatorFactory;
    }

    public async Task<CalculateRebateResult> Calculate(CalculateRebateRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        if (request.RebateIdentifier == null)
        {
            throw new ArgumentException("Rebate identifier cannot be null.");
        }

        if (request.ProductIdentifier == null)
        {
            throw new ArgumentException("Product identifier cannot be null.");
        }

        Rebate? rebate = await _rebateStore.Get(request.RebateIdentifier);

        Product? product = await _productStore.Get(request.ProductIdentifier);

        var result = new CalculateRebateResult();

        if (rebate != null && product != null && product.Supports(rebate))
        {
            var parameters = new RebateCalculationParameters(rebate, product, request.Volume);

            var calculator = _calculatorFactory.GetInstance(rebate.Incentive);

            if (calculator.Supports(parameters))
            {
                var calculation = calculator.Calculate(parameters);

                await _rebateCalculationStore.Create(calculation);

                result.Success = true;
                result.Amount = calculation.Amount;
            }
        }

        return result;
    }
}
