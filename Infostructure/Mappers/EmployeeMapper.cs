using BankDatabase.Entities;
using BankDatabase.Infostructure.Currents;

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
            CronWorkOrder = entity.CronWorkOrder
        };
    }

    public void ApplyToEntity(EmployeeEntity entity, CurrentEmployee current)
    {
        entity.Id = current.Id;
        entity.Name = current.Name;
        entity.Age = current.Age;
        entity.CronWorkOrder = current.CronWorkOrder;
    }
}
