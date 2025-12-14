using BankDatabase.Entities;
using Microsoft.EntityFrameworkCore;

namespace BankDatabase;

public class ApplicationContext : DbContext
{
    public DbSet<EmployeeEntity> Customers => Set<EmployeeEntity>();

    public ApplicationContext() 
    {
        Database.EnsureCreated();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=datamonitor.db");
    }
}
