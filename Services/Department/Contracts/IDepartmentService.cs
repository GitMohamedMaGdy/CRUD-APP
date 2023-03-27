using Task.Domain.Entities;

namespace Web.Services
{
    public interface IDepartmentService
    {
        Task<IEnumerable<Department>> GetDepartment();
        Task<Department> GetDepartmentByID(int ID);
        Task<Department> InsertDepartment(Department objDepartment);
        Task<Department> UpdateDepartment(Department objDepartment);
        Task<bool> DeleteDepartmentAsync(int ID);
    }
}
