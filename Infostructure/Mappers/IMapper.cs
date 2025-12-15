using BankDatabase.Entities;
using BankDatabase.Infostructure.Currents;

namespace BankDatabase.Infostructure.Mappers;

public interface IMapper<TEntity, TCurrent> where TEntity: IEntity where TCurrent: ICurrent
{
    public void ApplyToEntity(TEntity entity, TCurrent current);
    public TCurrent ToCurrent(TEntity entity);
}
