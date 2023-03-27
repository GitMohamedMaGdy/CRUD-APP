using Task.Domain.Entities;
using Task.Repository;

namespace Task.Repositories
{
    public class EmployeeRepository : Repository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(AppDbContext context) : base(context)
        {
        }


    }
}
