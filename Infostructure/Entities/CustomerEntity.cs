namespace BankDatabase.Infostructure.Entities;

public class CustomerEntity : IEntity
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string EmailAddress { get; set; } = string.Empty;
}
