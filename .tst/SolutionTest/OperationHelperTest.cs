namespace SolutionTest;

public class OperationHelperTest
{
    private readonly Mock<IEmployeeService> _mockService;
    private string[] _args;
    private OperationHelper _operationHelper;

    public OperationHelperTest()
    {
        _mockService = new Mock<IEmployeeService>();
    }

    [Fact]
    public async Task ProceedOperation_AddEmployee_CallsServiceAddEmployee()
    {
        SetValues(["-add", "FirstName:John", "LastName:Doe", "Salary:50000"]);
        
        _mockService.Setup(s => s.AddEmployee("John", "Doe", 50000)).Returns(Task.CompletedTask);

        await _operationHelper.ProceedOperation();
        
        _mockService.Verify(s => s.AddEmployee("John", "Doe", 50000), Times.Once);
    }

    [Fact]
    public async Task ProceedOperation_UpdateEmployee_CallsServiceUpdateEmployee()
    {
        SetValues(["-update"," Id:1","FirstName:Jane","LastName:Smith","Salary:60000"]);

        _mockService.Setup(s => s.UpdateEmployee(1, "Jane", "Smith", 60000)).Returns(Task.CompletedTask);

        await _operationHelper.ProceedOperation();

        _mockService.Verify(s => s.UpdateEmployee(1, "Jane", "Smith", 60000), Times.Once);
    }

    [Fact]
    public async Task ProceedOperation_GetEmployee_CallsServiceGetEmployeeById()
    {
        SetValues(["-get","Id:1"]);
            
        _mockService.Setup(s => s.GetEmployeeById(1)).Returns(new Employee { Id = 1, FirstName = "John", LastName = "Doe", SalaryPerHour = 50000 });

        await _operationHelper.ProceedOperation();

        _mockService.Verify(s => s.GetEmployeeById(1), Times.Once);
    }

    [Fact]
    public async Task ProceedOperation_DeleteEmployee_CallsServiceDeleteEmployee()
    {
        SetValues(["-delete", "Id:1"]);
        
        _mockService.Setup(s => s.DeleteEmployee(1)).Returns(Task.CompletedTask);
        
        await _operationHelper.ProceedOperation();

        _mockService.Verify(s => s.DeleteEmployee(1), Times.Once);
    }

    [Fact]
    public async Task ProceedOperation_GetAllEmployees_CallsServiceGetAllEmployees()
    {
        SetValues(["-getall"]);
        
        var employees = new[]
        {
            new Employee { Id = 1, FirstName = "John", LastName = "Doe", SalaryPerHour = 50000 },
            new Employee { Id = 2, FirstName = "Jane", LastName = "Smith", SalaryPerHour = 60000 }
        };
        _mockService.Setup(s => s.GetAllEmployees()).Returns(employees);
        
        await _operationHelper.ProceedOperation();
        
        _mockService.Verify(s => s.GetAllEmployees(), Times.Once);
    }

    [Fact]
    public async Task ProceedOperation_UnknownOperation_PrintsUnknownOperationMessage()
    {
        SetValues(["-unknown"]);

        using var consoleOutput = new ConsoleOutput();

        await _operationHelper.ProceedOperation();
        
        Assert.Contains("Unknown operation.", consoleOutput.GetOutput());
    }


    private void SetValues(string[] args)
    {
        _args = args;
        _operationHelper = new OperationHelper(_mockService.Object, _args);
    }
    
    private class ConsoleOutput : IDisposable
    {
        private readonly StringWriter _stringWriter;
        private readonly TextWriter _originalOutput;

        public ConsoleOutput()
        {
            _originalOutput = Console.Out;
            _stringWriter = new StringWriter();
            Console.SetOut(_stringWriter);
        }

        public string GetOutput() => _stringWriter.ToString();

        public void Dispose()
        {
            Console.SetOut(_originalOutput);
            _stringWriter.Dispose();
        }
    }
}