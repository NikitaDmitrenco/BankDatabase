using BankDatabase.Entities;

namespace BankDatabase.Repositories;

public class EmployeeRepository : BaseRepository<EmployeeEntity>
{
    public EmployeeRepository(ApplicationContext context) : base(context)
    {
    }
}
