namespace SolutionTest;

public class EmployeeServiceTest
{
    private readonly Mock<IUnitOfWork> _mockUnitOfWork;
    private readonly EmployeeService _employeeService;

    public EmployeeServiceTest()
    {
        _mockUnitOfWork = new Mock<IUnitOfWork>();
        _employeeService = new EmployeeService(_mockUnitOfWork.Object);
    }

    [Fact]
    public async Task AddEmployee_Should_Add_Employee_And_SaveChanges()
    {
        var employee = new Employee { FirstName = "John", LastName = "Doe", SalaryPerHour = 25m };
        
        _mockUnitOfWork.Setup(uow => uow.EmployeeRepository.AddEntity(It.IsAny<Employee>()));
        _mockUnitOfWork.Setup(uow => uow.SaveChangesAsync()).Returns(Task.CompletedTask);
        
        await _employeeService.AddEmployee(employee.FirstName, employee.LastName, employee.SalaryPerHour);
        
        _mockUnitOfWork.Verify(uow => uow.EmployeeRepository.AddEntity(It.IsAny<Employee>()), Times.Once);
        _mockUnitOfWork.Verify(uow => uow.SaveChangesAsync(), Times.Once);
    }

    [Fact]
    public async Task DeleteEmployee_Should_Delete_Employee_And_SaveChanges()
    {
        var employeeId = 1;

        _mockUnitOfWork.Setup(uow => uow.EmployeeRepository.DeleteEntity(employeeId));
        _mockUnitOfWork.Setup(uow => uow.SaveChangesAsync()).Returns(Task.CompletedTask);
        
        await _employeeService.DeleteEmployee(employeeId);
        
        _mockUnitOfWork.Verify(uow => uow.EmployeeRepository.DeleteEntity(employeeId), Times.Once);
        _mockUnitOfWork.Verify(uow => uow.SaveChangesAsync(), Times.Once);
    }

    [Fact]
    public void GetAllEmployees_Should_Return_All_Employees()
    {
        var employees = new List<Employee>
        {
            new() { FirstName = "John", LastName = "Doe", SalaryPerHour = 25m },
            new() { FirstName = "Jane", LastName = "Smith", SalaryPerHour = 30m }
        };

        _mockUnitOfWork.Setup(uow => uow.EmployeeRepository.GetAllEntities()).Returns(employees);
        
        var result = _employeeService.GetAllEmployees();
        
        Assert.Equal(employees, result);
    }

    [Fact]
    public void GetEmployeeById_Should_Return_Employee_By_Id()
    {
        var employee = new Employee { Id = 1, FirstName = "John", LastName = "Doe", SalaryPerHour = 25m };

        _mockUnitOfWork.Setup(uow => uow.EmployeeRepository.GetEntityById(employee.Id)).Returns(employee);
        
        var result = _employeeService.GetEmployeeById(employee.Id);
        
        Assert.Equal(employee, result);
    }

    [Fact]
    public async Task UpdateEmployee_Should_Update_Employee_And_SaveChanges()
    {
        var employee = new Employee { Id = 1, FirstName = "John", LastName = "Doe", SalaryPerHour = 25m };

        _mockUnitOfWork.Setup(uow => uow.EmployeeRepository.GetEntityById(employee.Id)).Returns(employee);
        _mockUnitOfWork.Setup(uow => uow.EmployeeRepository.UpdateEntity(employee));
        _mockUnitOfWork.Setup(uow => uow.SaveChangesAsync()).Returns(Task.CompletedTask);
        
        await _employeeService.UpdateEmployee(employee.Id, "Jane", "Smith", 30m);
        
        _mockUnitOfWork.Verify(uow => uow.EmployeeRepository.UpdateEntity(employee), Times.Once);
        _mockUnitOfWork.Verify(uow => uow.SaveChangesAsync(), Times.Once);
        Assert.Equal("Jane", employee.FirstName);
        Assert.Equal("Smith", employee.LastName);
        Assert.Equal(30m, employee.SalaryPerHour);
    }
}