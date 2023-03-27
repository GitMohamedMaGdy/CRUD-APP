using Task.Repositories;

namespace Task.Domain
{
    public interface IUnitOfWork : IDisposable
    {
        public IDepartmentRepository DepartmentRepository { get; }
        public IEmployeeRepository EmployeeRepository { get; }
        Task<int> SaveChangesAsync();
    }
}
