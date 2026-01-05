using Microsoft.EntityFrameworkCore;
using BankDatabase.Infostructure.Entities;

namespace BankDatabase.Infostructure.Repositories;

public class EmployeeRepository : BaseRepository<EmployeeEntity>
{
    private readonly IDbContextFactory<ApplicationContext> factory;

    public EmployeeRepository(IDbContextFactory<ApplicationContext> factory) : base(factory)
    {
        this.factory = factory;
    }
}
