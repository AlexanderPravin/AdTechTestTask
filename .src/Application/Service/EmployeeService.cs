namespace Application.Service;

public class EmployeeService(IUnitOfWork unitOfWork) : IEmployeeService
{
    public async Task AddEmployee(string firstName, string lastName, decimal salary)
    {
        var employee = new Employee
        {
            FirstName = firstName,
            LastName = lastName,
            SalaryPerHour = salary
        };

        unitOfWork.EmployeeRepository.AddEntity(employee);

        await unitOfWork.SaveChangesAsync();
    }

    public async Task DeleteEmployee(int id)
    {
        unitOfWork.EmployeeRepository.DeleteEntity(id);

        await unitOfWork.SaveChangesAsync();
    }


    public IEnumerable<Employee> GetAllEmployees() =>
        unitOfWork.EmployeeRepository.GetAllEntities();

    public Employee GetEmployeeById(int id) =>
        unitOfWork.EmployeeRepository.GetEntityById(id);

    public async Task UpdateEmployee(int id,
        string? firstName = null,
        string? lastName = null,
        decimal? salary = null)
    {
        var employee = unitOfWork.EmployeeRepository.GetEntityById(id)
                       ?? throw new Exception();

        if (!string.IsNullOrEmpty(firstName)) employee.FirstName = firstName;
        if (!string.IsNullOrEmpty(lastName)) employee.LastName = lastName;
        if (salary.HasValue) employee.SalaryPerHour = salary.Value;

        unitOfWork.EmployeeRepository.UpdateEntity(employee);

        await unitOfWork.SaveChangesAsync();
    }
}