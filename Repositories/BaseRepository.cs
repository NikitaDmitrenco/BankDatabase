namespace BankDatabase.Repositories;

public class BaseRepository<TEntity> where TEntity : class
{
    protected readonly ApplicationContext context;
    public BaseRepository(ApplicationContext context)
    {
        this.context = context;
    }

    public virtual List<TEntity> GetAll() 
    {
        var set = context.Set<TEntity>();
        return set.ToList();
    }

    public virtual void Add(TEntity entity) 
    {
        context.Add(entity);
        context.SaveChanges();
    }

    public virtual void Update(TEntity entity) 
    {
        context.Update(entity);
        context.SaveChanges();
    }

    public virtual void Delete(TEntity entity) 
    {
        context.Remove(entity);
        context.SaveChanges();
    }
}
