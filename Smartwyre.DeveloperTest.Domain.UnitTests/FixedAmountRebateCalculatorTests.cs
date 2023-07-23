using NUnit.Framework;
using Smartwyre.DeveloperTest.Domain.Entities;
using Smartwyre.DeveloperTest.Domain.Entities.RebateCalculations;
using Smartwyre.DeveloperTest.Domain.Exceptions;
using Smartwyre.DeveloperTest.Domain.Services;

namespace Smartwyre.DeveloperTest.Domain.UnitTests
{
    [TestFixture]
    public class FixedAmountRebateCalculatorTests
    {
        private FixedAmountRebateCalculator? _calculator;
        private Rebate? _rebate;
        private Product? _product;

        [SetUp]
        public void Setup()
        {
            _calculator = new FixedAmountRebateCalculator();
            _rebate = new Rebate()
            {
                Amount = 15.50m
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
        public void Calculate_WhenParametersValid_AmountIsExpected()
        {
            var parameters = new RebateCalculationParameters(_rebate!, _product!, 10);

            var calculation = _calculator!.Calculate(parameters);

            Assert.AreEqual(15.50m, calculation.Amount);
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
        public void Calculate_WhenUnsupportedParameters_ThrowsUnsupportedRebateCalculationException()
        {
            _rebate!.Amount = 0;

            var parameters = new RebateCalculationParameters(_rebate, _product!, 10);

            Assert.Throws<UnsupportedRebateCalculationException>(() => _calculator!.Calculate(parameters));
        }
    }
}