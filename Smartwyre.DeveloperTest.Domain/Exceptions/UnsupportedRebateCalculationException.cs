namespace Smartwyre.DeveloperTest.Domain.Exceptions;

public class UnsupportedRebateCalculationException : Exception
{
    private const string _defaultMessage = "Unsupported rebate calculation.";

    public UnsupportedRebateCalculationException() : base(_defaultMessage)
    {
    }

    public UnsupportedRebateCalculationException(string message) : base(message)
    {
    }
}
