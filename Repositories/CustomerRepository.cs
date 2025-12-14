using BankDatabase.Entities;

namespace BankDatabase.Repositories;

public class CustomerRepository : BaseRepository<CustomerEntity>
{
    public CustomerRepository(ApplicationContext context) : base(context)
    {
    }
}
