using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Smartwyre.DeveloperTest.Contracts;
using Smartwyre.DeveloperTest.Runner.ExtensionMethods;
using Smartwyre.DeveloperTest.Runner.Helpers;
using Smartwyre.DeveloperTest.Services;

public class Program
{
    private static ServiceProvider? _serviceProvider;

    public static void Main(string[] args)
    {
        MainAsync(args).GetAwaiter().GetResult();
    }

    private static async Task MainAsync(string[] _)
    {
        ConfigureServices();

        Console.ForegroundColor = ConsoleColor.Blue;

        ConsoleHelpers.Confirm("Enter calculate rebate request?");

        do
        {
            var request = GetCalculateRebateRequest();

            await CalculateRequest(request);
        }
        while (ConsoleHelpers.Confirm("Enter another calculate rebate request?"));
    }

    private static void ConfigureServices()
    {
        _serviceProvider = new ServiceCollection()
            .RegisterDomainTypes()
            .RegisterApplicationTypes()
            .RegisterInfrastructureTypes()
            .AddAutoMapper(
                typeof(Program),
                typeof(Smartwyre.DeveloperTest.Data.Mappings.AssemblyMarker))
            .BuildServiceProvider();
    }

    private static CalculateRebateRequest GetCalculateRebateRequest()
    {
        var request = new CalculateRebateRequest();

        Console.WriteLine("Enter rebate identifier:");
        request.RebateIdentifier = Console.ReadLine();

        Console.WriteLine("Enter product identifier:");
        request.ProductIdentifier = Console.ReadLine();

        request.Volume = ConsoleHelpers.GetDecimalInput("Enter volume:");

        return request;
    }

    static async Task CalculateRequest(CalculateRebateRequest request)
    {
        var rebateService = _serviceProvider!.GetService<IRebateService>();

        try
        {
            var result = await rebateService!.Calculate(request);
            PrintResult(result);
        }
        catch (Exception ex)
        {
            PrintResult(ex);
        }
    }

    static void PrintResult(object results)
    {
        Console.WriteLine();
        Console.WriteLine("Result");
        Console.WriteLine("========");
        Console.WriteLine();
        PrintJson(results);
        Console.WriteLine();
    }

    static void PrintJson(object obj)
    {
        Console.WriteLine(JsonConvert.SerializeObject(obj, Formatting.Indented));
    }
}
