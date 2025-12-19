using BankDatabase.Entities;
using Microsoft.EntityFrameworkCore;

namespace BankDatabase.Repositories;

public class CustomerRepository : BaseRepository<CustomerEntity>
{
    private IDbContextFactory<ApplicationContext> factory;

    public CustomerRepository(IDbContextFactory<ApplicationContext> factory) : base(factory)
    {
        this.factory = factory;
    }

    public override List<CustomerEntity> GetAll()
    {
        using (var context = factory.CreateDbContext())
        {
            var set = context.Set<CustomerEntity>();
            return set.ToList();
        }
    }
}
