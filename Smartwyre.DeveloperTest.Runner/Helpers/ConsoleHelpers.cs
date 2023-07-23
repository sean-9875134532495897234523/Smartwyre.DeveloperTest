namespace Smartwyre.DeveloperTest.Runner.Helpers;

internal static class ConsoleHelpers
{
    public static bool Confirm(string statement)
    {
        ConsoleKey response;
        do
        {
            Console.Write($"{ statement } [y/n] ");
            response = Console.ReadKey(false).Key;
            if (response != ConsoleKey.Enter)
            {
                Console.WriteLine();
            }
        } while (response != ConsoleKey.Y && response != ConsoleKey.N);

        return (response == ConsoleKey.Y);
    }

    public static decimal GetDecimalInput(string statement)
    {
        decimal? num = null;

        do
        {
            Console.WriteLine(statement);

            string? input = Console.ReadLine();

            if (decimal.TryParse(input, out decimal parsed))
            {
                num = parsed;
            }
            else
            {
                Console.WriteLine("Invalid input! Must be numberic.");
            }
        } while (!num.HasValue);

        return num.Value;
    }
}
