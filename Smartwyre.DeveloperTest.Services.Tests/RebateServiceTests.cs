using Moq;
using NUnit.Framework;
using Smartwyre.DeveloperTest.Contracts;
using Smartwyre.DeveloperTest.Domain.Entities;
using Smartwyre.DeveloperTest.Domain.Entities.RebateCalculations;
using Smartwyre.DeveloperTest.Domain.Enums;
using Smartwyre.DeveloperTest.Domain.Factories;
using Smartwyre.DeveloperTest.Domain.Services;
using Smartwyre.DeveloperTest.Domain.Stores;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Smartwyre.DeveloperTest.Services.Tests;

[TestFixture]
internal class RebateServiceTests
{
    private Mock<IStore<Rebate>>? _rebateStore;
    private Mock<IStore<Product>>? _productStore;
    private Mock<IStore<RebateCalculation>>? _rebateCalculationStore;
    private Mock<IRebateCalculator>? _rebateCalculator;
    private Mock<IRebateCalculatorFactory>? _rebateCalculatorFactory;

    private RebateService? _rebateService;
    private CalculateRebateRequest? _calculateRequest;
    private Rebate? _rebate;
    private Product? _product;
    private RebateCalculation? _returnedCalculation;
    private RebateCalculation? _savedCalculation;

    [SetUp]
    public void SetUp()
    {
        _savedCalculation = null;
        _rebate = new Rebate();
        _rebateStore = new Mock<IStore<Rebate>>();
        _rebateStore.Setup(s => s.Get(It.IsAny<string>())).ReturnsAsync(() => _rebate);

        _product = new Product();
        _productStore = new Mock<IStore<Product>>();
        _productStore.Setup(s => s.Get(It.IsAny<string>())).ReturnsAsync(() => _product);

        _rebateCalculationStore = new Mock<IStore<RebateCalculation>>();
        _rebateCalculationStore.Setup(s => s.Create(It.IsAny<RebateCalculation>())).Callback((RebateCalculation calculation) =>
        {
            _savedCalculation = calculation;
        }).Returns(Task.CompletedTask);

        _returnedCalculation = new TestRebateCalculation() { Amount = 150m };

        _rebateCalculator = new Mock<IRebateCalculator>();
        _rebateCalculator.Setup(c => c.Supports(It.IsAny<RebateCalculationParameters>())).Returns(true);
        _rebateCalculator.Setup(c => c.Calculate(It.IsAny<RebateCalculationParameters>())).Returns(_returnedCalculation);

        _rebateCalculatorFactory = new Mock<IRebateCalculatorFactory>();
        _rebateCalculatorFactory.Setup(f => f.GetInstance(It.IsAny<IncentiveType>())).Returns(_rebateCalculator.Object);

        _rebateService = new RebateService(
            _rebateStore.Object,
            _productStore.Object,
            _rebateCalculationStore.Object,
            _rebateCalculatorFactory.Object);

        _calculateRequest = DefaultRequest();
    }

    [Test]
    public void Calculate_WhenRequestNull_ThrowsArgumentNullException()
    {
        Assert.ThrowsAsync<ArgumentNullException>(() => _rebateService!.Calculate(null));
    }

    [Test]
    public void Calculate_WhenRebateIdentifierNull_ThrowsArgumentException()
    {
        _calculateRequest!.RebateIdentifier = null;

        Assert.ThrowsAsync<ArgumentException>(() => _rebateService!.Calculate(_calculateRequest));
    }

    [Test]
    public async Task Calculate_WhenRebateIdentifierNull_DoesNotCallRebateStore()
    {
        _calculateRequest!.RebateIdentifier = null;

        try
        {
            _ = await _rebateService!.Calculate(_calculateRequest);
        }
        catch (Exception) { }

        _rebateStore!.Verify(s => s.Get(It.IsAny<string>()), Times.Never);
    }

    [Test]
    public void Calculate_WhenProductIdentifierNull_ThrowsArgumentException()
    {
        _calculateRequest!.ProductIdentifier = null;

        Assert.ThrowsAsync<ArgumentException>(() => _rebateService!.Calculate(_calculateRequest));
    }

    [Test]
    public async Task Calculate_WhenProductIdentifierNull_DoesNotCallRebateStore()
    {
        _calculateRequest!.ProductIdentifier = null;

        try
        {
            _ = await _rebateService!.Calculate(_calculateRequest);
        }
        catch (Exception) { }

        _productStore!.Verify(s => s.Get(It.IsAny<string>()), Times.Never);
    }

    [Test]
    public async Task Calculate_WhenRebateNotFound_IsUnsuccessful()
    {
        _rebate = null;

        var result = await _rebateService!.Calculate(_calculateRequest!);

        AssertUnsuccessfulResult(result);
    }

    [Test]
    public async Task Calculate_WhenProductNotFound_IsUnsuccessful()
    {
        _product = null;

        var result = await _rebateService!.Calculate(_calculateRequest!);

        AssertUnsuccessfulResult(result);
    }

    [TestCase(IncentiveType.FixedCashAmount)]
    [TestCase(IncentiveType.FixedRateRebate)]
    [TestCase(IncentiveType.AmountPerUom)]
    public async Task Calculate_WhenProductDoesNotSupportIncentive_IsUnsuccessful(IncentiveType rebateIncentive)
    {
        _rebate!.Incentive = rebateIncentive;

        _product!.SupportedIncentives = Enum.GetValues(typeof(IncentiveType))
            .Cast<IncentiveType>()
            .Where(x => x != rebateIncentive);

        var result = await _rebateService!.Calculate(_calculateRequest!);

        AssertUnsuccessfulResult(result);
    }

    [TestCase(IncentiveType.FixedCashAmount)]
    [TestCase(IncentiveType.FixedRateRebate)]
    [TestCase(IncentiveType.AmountPerUom)]
    public async Task Calculate_PassesTheRightIncentiveTypeToCalculatorFactory(IncentiveType rebateIncentive)
    {
        _rebate!.Incentive = rebateIncentive;

        _product!.SupportedIncentives = Enum.GetValues(typeof(IncentiveType))
            .Cast<IncentiveType>();

        var result = await _rebateService!.Calculate(_calculateRequest!);

        _rebateCalculatorFactory!.Verify(f => f.GetInstance(It.IsAny<IncentiveType>()), Times.Once);
        _rebateCalculatorFactory!.Verify(f => f.GetInstance(rebateIncentive), Times.Once);
    }

    public async Task Calculate_WhenCalculatorDoesNotSupport_ResultIsUnsuccessful()
    {
        _rebateCalculator!.Setup(c => c.Supports(It.IsAny<RebateCalculationParameters>())).Returns(false);

        var result = await _rebateService!.Calculate(_calculateRequest!);

        AssertUnsuccessfulResult(result);
    }

    public async Task Calculate_WhenCalculatorDoesNotSupport_CalculatorCalculateIsNotCalled()
    {
        _rebateCalculator!.Setup(c => c.Supports(It.IsAny<RebateCalculationParameters>())).Returns(false);

        var result = await _rebateService!.Calculate(_calculateRequest!);

        _rebateCalculator.Verify(c => c.Calculate(It.IsAny<RebateCalculationParameters>()), Times.Never);
    }

    public async Task Calculate_WhenCalculatorDoesSupport_ResultIsSuccessful()
    {
        var result = await _rebateService!.Calculate(_calculateRequest!);

        Assert.IsTrue(result.Success);
    }

    public async Task Calculate_WhenCalculatorDoesSupport_CalculatedAmountIsReturnedInResult()
    {
        var result = await _rebateService!.Calculate(_calculateRequest!);

        Assert.AreEqual(150m, result.Amount);
    }

    public async Task Calculate_WhenCalculatorDoesSupport_CalculatedAmountIsSaved()
    {
        _ = await _rebateService!.Calculate(_calculateRequest!);

        Assert.AreEqual(150m, _savedCalculation);
    }

    private static CalculateRebateRequest DefaultRequest()
    {
        return new CalculateRebateRequest()
        {
            ProductIdentifier = "test-product-id",
            RebateIdentifier = "test-rebate-id",
            Volume = 10
        };
    }

    private void AssertUnsuccessfulResult(CalculateRebateResult result)
    {
        Assert.IsFalse(result.Success);
        Assert.IsNull(result.Amount);
        Assert.IsNull(_savedCalculation);
    }

    private class TestRebateCalculation : RebateCalculation { }
}