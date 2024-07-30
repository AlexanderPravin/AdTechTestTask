namespace Domain.Interfaces;

public interface IUnitOfWork
{
    public IRepository<Employee> EmployeeRepository { get; }

    public Task SaveChangesAsync();
}