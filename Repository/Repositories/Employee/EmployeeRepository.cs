using Task.Domain.Entities;
using Task.Repository;

namespace Task.Repositories
{
    public class DepartmentRepository : Repository<Department>, IDepartmentRepository
    {
        public DepartmentRepository(AppDbContext context) : base(context)
        {
        }


    }
}
