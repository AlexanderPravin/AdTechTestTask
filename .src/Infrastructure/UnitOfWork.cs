namespace Infrastructure;

public class UnitOfWork(IRepository<Employee> employeeRepository) : IUnitOfWork
{
    public IRepository<Employee> EmployeeRepository { get; } = employeeRepository;

    public async Task SaveChangesAsync()
    {
        await EmployeeRepository.SaveChangesAsync();
    }
}