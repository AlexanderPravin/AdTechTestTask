namespace Domain.Interfaces;

public interface IEmployeeService
{
    Task AddEmployee(string firstName, string lastName, decimal salary);
    Task DeleteEmployee(int id);
    IEnumerable<Employee> GetAllEmployees();
    Employee GetEmployeeById(int id);

    Task UpdateEmployee(int id,
        string? firstName = null,
        string? lastName = null,
        decimal? salary = null);
}