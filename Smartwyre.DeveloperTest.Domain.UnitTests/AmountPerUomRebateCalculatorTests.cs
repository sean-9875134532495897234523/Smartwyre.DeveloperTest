using NUnit.Framework;
using Smartwyre.DeveloperTest.Domain.Entities;
using Smartwyre.DeveloperTest.Domain.Entities.RebateCalculations;
using Smartwyre.DeveloperTest.Domain.Exceptions;
using Smartwyre.DeveloperTest.Domain.Services;

namespace Smartwyre.DeveloperTest.Domain.UnitTests;

[TestFixture]
public class AmountPerUomRebateCalculatorTests
{
    private AmountPerUomRebateCalculator? _calculator;
    private Rebate? _rebate;
    private Product? _product;

    [SetUp]
    public void Setup()
    {
        _calculator = new AmountPerUomRebateCalculator();
        _rebate = new Rebate()
        {
            Amount = 25.72m
        };
        _product = new Product();
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
    public void Supports_WhenRebateAmountIsZero_ReturnsFalse()
    {
        _rebate!.Amount = 0;

        var parameters = new RebateCalculationParameters(_rebate, _product!, 10);

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

    [TestCase(2, 3, 6)]
    [TestCase(75.99, 304.6, 23146.554)]
    public void Calculate_WhenParametersValid_AmountIsExpected(decimal rebateAmount, decimal volume, decimal expectedAmount)
    {
        _rebate!.Amount = rebateAmount;

        var parameters = new RebateCalculationParameters(_rebate, _product!, volume);

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
    public void Calculate_WhenParametersValid_CalculationIncludesVolume()
    {
        var parameters = new RebateCalculationParameters(_rebate!, _product!, 10);

        var calculation = _calculator!.Calculate(parameters);

        Assert.AreEqual(10, (calculation as AmountPerUomRebateCalculation)!.Volume);
    }

    [Test]
    public void Calculate_WhenUnsupportedParameters_ThrowsUnsupportedRebateCalculationException()
    {
        _rebate!.Amount = 0;

        var parameters = new RebateCalculationParameters(_rebate, _product!, 10);

        Assert.Throws<UnsupportedRebateCalculationException>(() => _calculator!.Calculate(parameters));
    }
}
