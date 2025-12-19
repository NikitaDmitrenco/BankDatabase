using BankDatabase.Entities;
using BankDatabase;
using Microsoft.EntityFrameworkCore;

namespace BankDatabase.Repositories;

public class EmployeeRepository : BaseRepository<EmployeeEntity>
{
    private readonly IDbContextFactory<ApplicationContext> factory;
    public EmployeeRepository(IDbContextFactory<ApplicationContext> factory) : base(factory)
    {
        this.factory = factory;
    }
}
