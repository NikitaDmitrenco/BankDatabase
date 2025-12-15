namespace BankDatabase.Entities;

public class EmployeeEntity : IEntity
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Age { get; set; }
    public string CronWorkOrder { get; set; } = string.Empty;
}
