namespace SolutionTest;

public class EmployeeRepositoryTest
{
    private readonly Mock<IFileHandler> _fileHandlerMock;
    private readonly EmployeeRepository _employeeRepository;
    private readonly List<Employee> _testEmployees;

    public EmployeeRepositoryTest()
    {
        _fileHandlerMock = new Mock<IFileHandler>();
        _testEmployees = new List<Employee>
        {
            new Employee { Id = 1, FirstName = "John", LastName = "Doe", SalaryPerHour = 25.5m },
            new Employee { Id = 2, FirstName = "Jane", LastName = "Smith", SalaryPerHour = 30.0m }
        };
        _fileHandlerMock.Setup(fh => fh.GetText()).ReturnsAsync(JsonSerializer.Serialize(_testEmployees));
        _employeeRepository = new EmployeeRepository(_fileHandlerMock.Object);
    }

    [Fact]
    public void AddEntity_ShouldAddEntity()
    {
        var newEmployee = new Employee { FirstName = "New", LastName = "Employee", SalaryPerHour = 20.0m };
        _employeeRepository.AddEntity(newEmployee);
        Assert.Contains(newEmployee, _employeeRepository.GetAllEntities());
        Assert.Equal(3, newEmployee.Id);
    }

    [Fact]
    public void DeleteEntity_ShouldDeleteEntity()
    {
        _employeeRepository.DeleteEntity(1);
        Assert.DoesNotContain(_testEmployees.First(e => e.Id == 1), _employeeRepository.GetAllEntities());
    }

    [Fact]
    public void UpdateEntity_ShouldUpdateEntity()
    {
        var updatedEmployee = new Employee { Id = 1, FirstName = "Updated", LastName = "Name", SalaryPerHour = 35.0m };
        _employeeRepository.UpdateEntity(updatedEmployee);
        var employee = _employeeRepository.GetEntityById(1);
        Assert.Equal("Updated", employee.FirstName);
        Assert.Equal("Name", employee.LastName);
        Assert.Equal(35.0m, employee.SalaryPerHour);
    }

    [Fact]
    public void GetAllEntities_ShouldReturnAllEntities()
    {
        var allEntities = _employeeRepository.GetAllEntities();
        Assert.Equal(2, allEntities.Count());
    }

    [Fact]
    public void GetEntityById_ShouldReturnEntity()
    {
        var employee = _employeeRepository.GetEntityById(1);
        Assert.Equal("John", employee.FirstName);
        Assert.Equal("Doe", employee.LastName);
        Assert.Equal(25.5m, employee.SalaryPerHour);
    }

    [Fact]
    public async Task SaveChangesAsync_ShouldSaveChanges()
    {
        string capturedJson = string.Empty;
        _fileHandlerMock.Setup(fh => fh.SetText(It.IsAny<string>()))
            .Callback<string>(json => capturedJson = json);
        
        await _employeeRepository.SaveChangesAsync();

        _fileHandlerMock.Verify(fh => fh.SetText(It.IsAny<string>()), Times.Once);

        var deserializedEmployees = JsonSerializer.Deserialize<List<Employee>>(capturedJson);
        Assert.NotNull(deserializedEmployees);
        Assert.Equal(2, deserializedEmployees.Count);
    }
}