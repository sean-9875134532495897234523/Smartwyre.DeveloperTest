using NUnit.Framework;
using Smartwyre.DeveloperTest.Domain.Entities;
using Smartwyre.DeveloperTest.Domain.Entities.RebateCalculations;
using Smartwyre.DeveloperTest.Domain.Exceptions;
using Smartwyre.DeveloperTest.Domain.Services;


namespace Smartwyre.DeveloperTest.Domain.UnitTests;

[TestFixture]
public class FixedRateRebateCalculatorTests
{
    private FixedRateRebateCalculator? _calculator;
    private Rebate? _rebate;
    private Product? _product;

    [SetUp]
    public void Setup()
    {
        _calculator = new FixedRateRebateCalculator();
        _rebate = new Rebate()
        {
            Percentage = 30
        };
        _product = new Product()
        {
            Price = 12.50m
        };
    }

    [Test]
    public void Supports_WhenValidParameters_ReturnsTrue()
    {
        var parameters = new RebateCalculationParameters(_rebate!, _product!, 10);

        bool supported = _calculator!.Supports(parameters);

        Assert.IsTrue(supported);
    }

    [Test]
    public void Supports_WhenRebateIsNull_ReturnsFalse()
    {
        var parameters = new RebateCalculationParameters(null, _product!, 10);

        bool supported = _calculator!.Supports(parameters);

        Assert.IsFalse(supported);
    }

    [Test]
    public void Supports_WhenProductIsNull_ReturnsFalse()
    {
        var parameters = new RebateCalculationParameters(_rebate!, null, 10);

        bool supported = _calculator!.Supports(parameters);

        Assert.IsFalse(supported);
    }

    [Test]
    public void Supports_WhenRebatePercentageIsZero_ReturnsFalse()
    {
        _rebate!.Percentage = 0;

        var parameters = new RebateCalculationParameters(_rebate, _product!, 10);

        bool supported = _calculator!.Supports(parameters);

        Assert.IsFalse(supported);
    }

    [Test]
    public void Supports_WhenProductPriceIsZero_ReturnsFalse()
    {
        _product!.Price = 0;

        var parameters = new RebateCalculationParameters(_rebate!, _product, 10);

        bool supported = _calculator!.Supports(parameters);

        Assert.IsFalse(supported);
    }

    [Test]
    public void Supports_WhenVolumeIsZero_ReturnsFalse()
    {
        var parameters = new RebateCalculationParameters(_rebate!, _product!, 0);

        bool supported = _calculator!.Supports(parameters);

        Assert.IsFalse(supported);
    }

    [TestCase(2, 3, 5, 30)]
    [TestCase(75.99, 17, 304.6, 393491.418)]
    public void Calculate_WhenParametersValid_AmountIsExpected(decimal price, decimal percentage, decimal volume, decimal expectedAmount)
    {
        _product!.Price = price;
        _rebate!.Percentage = percentage;

        var parameters = new RebateCalculationParameters(_rebate, _product, volume);

        var calculation = _calculator!.Calculate(parameters);

        Assert.AreEqual(expectedAmount, calculation.Amount);
    }

    [Test]
    public void Calculate_WhenParametersValid_CalculationIncludesRebateIdentifier()
    {
        _rebate!.Identifier = "test-rebate";

        var parameters = new RebateCalculationParameters(_rebate, _product!, 10);

        var calculation = _calculator!.Calculate(parameters);

        Assert.AreEqual("test-rebate", calculation.RebateIdentifier);
    }

    [Test]
    public void Calculate_WhenParametersValid_CalculationIncludesProductIdentifier()
    {
        _product!.Identifier = "test-product";

        var parameters = new RebateCalculationParameters(_rebate!, _product, 10);

        var calculation = _calculator!.Calculate(parameters);

        Assert.AreEqual("test-product", calculation.ProductIdentifier);
    }

    [Test]
    public void Calculate_WhenParametersValid_CalculationIncludesProductPrice()
    {
        var parameters = new RebateCalculationParameters(_rebate!, _product!, 10);

        var calculation = _calculator!.Calculate(parameters);

        Assert.AreEqual(12.50m, (calculation as FixedRateRebateCalculation)!.ProductPrice);
    }

    [Test]
    public void Calculate_WhenParametersValid_CalculationIncludesVolume()
    {
        var parameters = new RebateCalculationParameters(_rebate!, _product!, 10);

        var calculation = _calculator!.Calculate(parameters);

        Assert.AreEqual(10, (calculation as FixedRateRebateCalculation)!.Volume);
    }

    [Test]
    public void Calculate_WhenUnsupportedParameters_ThrowsUnsupportedRebateCalculationException()
    {
        _rebate!.Percentage = 0;

        var parameters = new RebateCalculationParameters(_rebate, _product!, 10);

        Assert.Throws<UnsupportedRebateCalculationException>(() => _calculator!.Calculate(parameters));
    }
}
