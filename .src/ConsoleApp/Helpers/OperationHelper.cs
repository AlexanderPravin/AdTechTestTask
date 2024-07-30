namespace ConsoleApp.Helpers;

public class OperationHelper(IEmployeeService service, string[] args)
{
    public async Task ProceedOperation()
    {
        var operations = new Dictionary<string, Func<Task>>
        {
            { "-add", AddEmployee },
            { "-update", UpdateEmployee },
            { "-get", GetEmployee },
            { "-delete", DeleteEmployee },
            { "-getall", GetAllEmployees }
        };
        
        if (operations.TryGetValue(args[0], out var operation)) 
            await operation();
        else 
            Console.WriteLine("Unknown operation.");
    }

    private async Task AddEmployee()
    {
        var firstName = args[1].Split(':')[1];
        var lastName = args[2].Split(':')[1];
        var salary = decimal.Parse(args[3].Split(':')[1], CultureInfo.InvariantCulture);

        await service.AddEmployee(firstName, lastName, salary);
    }

    private async Task UpdateEmployee()
    {
        var id = int.Parse(args[1].Split(':')[1]);
        var firstName = args.Length > 2 ? args[2].Split(':')[1] : null;
        var lastName = args.Length > 3 ? args[3].Split(':')[1] : null;
        var salary = args.Length > 4 ? decimal.Parse(args[4].Split(':')[1], CultureInfo.InvariantCulture) : (decimal?)null;
        
        await service.UpdateEmployee(id, firstName, lastName, salary);
    }

    private Task GetEmployee()
    {
        var id = int.Parse(args[1].Split(':')[1]);
        var employee = service.GetEmployeeById(id);
        Console.WriteLine(
            $"Id = {employee.Id}, FirstName = {employee.FirstName}, LastName = {employee.LastName}, SalaryPerHour = {employee.SalaryPerHour}");
        
        return Task.CompletedTask;
    }

    private async Task DeleteEmployee()
    {
        var id = int.Parse(args[1].Split(':')[1]);
        await service.DeleteEmployee(id);
    }

    private Task GetAllEmployees()
    {
        var employees = service.GetAllEmployees();
        
        foreach (var employee in employees)
        {
            Console.WriteLine(
                $"Id = {employee.Id}, FirstName = {employee.FirstName}, LastName = {employee.LastName}, SalaryPerHour = {employee.SalaryPerHour}");
        }
        
        return Task.CompletedTask;
    }
}