using BankDatabase.Infostructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace BankDatabase;

public class ApplicationContext : DbContext
{
    public DbSet<EmployeeEntity> Employees => Set<EmployeeEntity>();
    public DbSet<CustomerEntity> Customers => Set<CustomerEntity>();

    public ApplicationContext() 
    {
        Database.EnsureCreated();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=bankdatabase.db");
    }
}
