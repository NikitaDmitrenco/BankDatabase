using BankDatabase.Infostructure.Currents;
using BankDatabase.Infostructure.Entities;

namespace BankDatabase.Infostructure.Mappers;

public class CustomerMapper : IMapper<CustomerEntity, CurrentCustomer>
{
    public CurrentCustomer ToCurrent(CustomerEntity entity)
    {
        return new CurrentCustomer()
        {
            Id = entity.Id,
            Name = entity.Name,
            PhoneNumber = entity.PhoneNumber,
            EmailAddress = entity.EmailAddress,
        };
    }

    public void ApplyToEntity(CustomerEntity entity, CurrentCustomer current) 
    {
        entity.Id = current.Id;
        entity.Name = current.Name;
        entity.PhoneNumber = current.PhoneNumber;
        entity.EmailAddress = current.EmailAddress;
    }
}
