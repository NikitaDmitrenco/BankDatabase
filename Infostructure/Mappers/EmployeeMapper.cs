using BankDatabase.Infostructure.Currents;
using BankDatabase.Infostructure.Entities;

namespace BankDatabase.Infostructure.Mappers;

public class EmployeeMapper : IMapper<EmployeeEntity, CurrentEmployee>
{
    public CurrentEmployee ToCurrent(EmployeeEntity entity)
    {
        return new CurrentEmployee()
        {
            Id = entity.Id,
            Name = entity.Name,
            Age = entity.Age,
            PhoneNumber = entity.PhoneNumber,
            Address = entity.Address,
            Position = entity.Position,
        };
    }

    public void ApplyToEntity(EmployeeEntity entity, CurrentEmployee current)
    {
        entity.Id = current.Id;
        entity.Name = current.Name;
        entity.Age = current.Age;
        entity.PhoneNumber = current.PhoneNumber;
        entity.Address = current.Address;
        entity.Position = current.Address;
    }
}
