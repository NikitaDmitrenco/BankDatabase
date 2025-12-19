using BankDatabase.Entities;
using Microsoft.EntityFrameworkCore;

namespace BankDatabase.Tests;

public class BaseRepository <T> where T : class, IEntity
{
    public virtual IDbContextFactory<ApplicationContext> factory { get; set; }
    protected BaseRepository(IDbContextFactory<ApplicationContext> factory)
    {
        this.factory = factory;
    }

    public virtual T Update(T entity)
    {
        using (var context = factory.CreateDbContext())
        {
            context.Update(entity);
            context.SaveChanges();
            return entity;
        }
    }
    public virtual T? Get(T entity)
    {
        using (var context = factory.CreateDbContext())
        {
            var set = context.Set<T>();
            return set.Find(entity);
        }
    }
    public virtual List<T> GetAll(T entity)
    {
        using (var context = factory.CreateDbContext())
        {
            var set = context.Set<T>();
            return set.ToList();
        }
    }
    public virtual void Delete(T entity)
    {
        using (var context = factory.CreateDbContext())
        {
            context.Remove(entity);
            context.SaveChanges();
        }
    }
}

public class CustomerRepository : BaseRepository<CustomerEntity>
{
    public CustomerRepository(IDbContextFactory<ApplicationContext> factory) : base(factory)
    {
        
    }

    public override List<CustomerEntity> GetAll(CustomerEntity entity)
    {
        using (var context = factory.CreateDbContext())
        {
            var set = context.Set<CustomerEntity>();
            return set.OrderBy(x => x.Id).ToList();
        }
    }
}
