using ConsoleApp.Helpers;

namespace ConsoleApp;

internal class Program
{
    private static IEmployeeService? _service;

    private static async Task Main(string[] args)
    {
        try
        {
            _service = DIHelper.GetService<IEmployeeService>();

            if (args.Length == 0)
            {
                Console.WriteLine("No arguments provided");
                return;
            }

            var operationHelper = new OperationHelper(_service, args);

            await operationHelper.ProceedOperation();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }
}