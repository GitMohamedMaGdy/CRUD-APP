using Task.Domain;
using Task.Repositories;

namespace Task.Repository.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        protected readonly AppDbContext Context;
        public IDepartmentRepository DepartmentRepository { get; }
        public IEmployeeRepository EmployeeRepository { get; }
        public UnitOfWork(AppDbContext context)
        {
            Context = context;
            DepartmentRepository = new DepartmentRepository(Context);
            EmployeeRepository = new EmployeeRepository(Context);
        }
        public async Task<int> SaveChangesAsync()
        {
            return await Context.SaveChangesAsync();
        }

        public void Dispose()
        {
            Context.Dispose();
        }
    }
}