namespace BankDatabase.Infostructure.Entities;

public class EmployeeEntity : IEntity
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Age { get; set; }
    public string Address { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string Position { get; set; } = string.Empty;
}
