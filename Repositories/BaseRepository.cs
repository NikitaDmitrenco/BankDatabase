using BankDatabase.Entities;
using Microsoft.EntityFrameworkCore;

namespace BankDatabase.Repositories;

public class BaseRepository<TEntity> where TEntity : class, IEntity
{
    private readonly IDbContextFactory<ApplicationContext> factory;

    public BaseRepository(IDbContextFactory<ApplicationContext> factory)
    {
        this.factory = factory;
    }

    public virtual List<TEntity> GetAll() 
    {
        using (var context = factory.CreateDbContext()) 
        {
            var set = context.Set<TEntity>();
            return set.ToList();
        }
    }

    public virtual void Add(TEntity entity) 
    {
        using (var context = factory.CreateDbContext())
        {
            context.Add(entity);
            context.SaveChanges();
        }
    }

    public virtual void Update(TEntity entity) 
    {
        using (var context = factory.CreateDbContext())
        {
            context.Update(entity);
            context.SaveChanges();
        }
    }

    public virtual void Delete(TEntity entity) 
    {
        using (var context = factory.CreateDbContext()) 
        {
            context.Remove(entity);
            context.SaveChanges();
        }
    }
}
