using Task.Domain;
using Task.Domain.Entities;
using Task.Repositories;

namespace Task.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IUnitOfWork _unitOfWork;

        public EmployeeService(IEmployeeRepository employeeRepository, IUnitOfWork unitOfWork)
        {
            _employeeRepository = employeeRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Employee>> GetEmployees()
        {
            return await _employeeRepository.GetAllAsync();
        }
        public async Task<Employee> GetEmployeeByID(int ID)
        {
            return await _employeeRepository.GetByIdAsync(ID);
        }
        public async Task<Employee> InsertEmployee(Employee employee)
        {
            _employeeRepository.Add(employee);
            await _unitOfWork.SaveChangesAsync();
            return employee;
        }
        public async Task<Employee> UpdateEmployee(Employee employee)
        {
            _employeeRepository.Update(employee);
            await _unitOfWork.SaveChangesAsync();
            return employee;
        }
        public async Task<bool> DeleteEmployeeAsync(int ID)
        {
            bool result = false;
            var employee = await _employeeRepository.GetByIdAsync(ID);
            if (employee != null)
            {
                _employeeRepository.Remove(employee);
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
