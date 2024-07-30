namespace Infrastructure.Repositories;

public class EmployeeRepository(IFileHandler fileHandler) : BaseRepository<Employee>(fileHandler);