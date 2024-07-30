namespace Domain.Entities;

public sealed class Employee : BaseEntity
{
    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public decimal SalaryPerHour { get; set; }
}