using BankDatabase.Infostructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace BankDatabase.Infostructure.Repositories;

public class BaseRepository<TEntity> where TEntity : class, IEntity
{
    private readonly IDbContextFactory<ApplicationContext> factory;

    public BaseRepository(IDbContextFactory<ApplicationContext> factory)
    {
        this.factory = factory;
    }

    public virtual List<TEntity> GetAll() 
    {
        var context = factory.CreateDbContext();

        var set = context.Set<TEntity>();
        return set.ToList();
    }

    public virtual long Add(TEntity entity) 
    {
        var context = factory.CreateDbContext();

        var result = context.Add(entity);
        context.SaveChanges();
        return result.Entity.Id;
    }

    public virtual long Update(TEntity entity) 
    {
        var context = factory.CreateDbContext();

        var result = context.Update(entity);
        context.SaveChanges();
        return result.Entity.Id;
    }

    public virtual void Delete(TEntity entity) 
    {
        var context = factory.CreateDbContext();

        context.Remove(entity);
        context.SaveChanges();
    }
}
