using Task.Domain;
using Task.Domain.Entities;
using Task.Repositories;
using Web.Services;

namespace Task.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DepartmentService(IDepartmentRepository departmentRepository, IUnitOfWork unitOfWork)
        {
            _departmentRepository = departmentRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Department>> GetDepartment()
        {
            return await _departmentRepository.GetAllAsync();
        }
        public async Task<Department> GetDepartmentByID(int ID)
        {
            return await _departmentRepository.GetByIdAsync(ID);
        }
        public async Task<Department> InsertDepartment(Department objDepartment)
        {
            _departmentRepository.Add(objDepartment);
            await _unitOfWork.SaveChangesAsync();
            return objDepartment;
        }
        public async Task<Department> UpdateDepartment(Department objDepartment)
        {
            _departmentRepository.Update(objDepartment);
            await _unitOfWork.SaveChangesAsync();
            return objDepartment;
        }
        public async Task<bool> DeleteDepartmentAsync(int ID)
        {
            bool result = false;
            var department = await _departmentRepository.GetByIdAsync(ID);
            if (department != null)
            {
                _departmentRepository.Remove(department);
                await _unitOfWork.SaveChangesAsync();
                result = true;
            }
            else
            {
                result = false;
            }
            return result;
        }

    }
}
