using RestarauntSystem.Core.Models;
using RestarauntSystem.Core.Repositories;
using RestarauntSystem.Core.Services;


namespace RestarauntSystem.Infrastructure.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IPositionRepository _positionRepository;

        public EmployeeService(
            IEmployeeRepository employeeRepository,
            IPositionRepository positionRepository)
        {
            _employeeRepository = employeeRepository;
            _positionRepository = positionRepository;
        }

        public async Task<IEnumerable<Employee>> GetAllEmployeesAsync()
        {
            return await _employeeRepository.GetAllAsync();
        }

        public async Task<Employee> GetEmployeeDetailsAsync(int employeeId)
        {
            return await _employeeRepository.GetByIdAsync(employeeId);
        }

        public async Task<Employee> CreateEmployeeAsync(Employee employee)
        {
            return await _employeeRepository.AddAsync(employee);
        }

        public async Task UpdateEmployeeAsync(Employee employee)
        {
            await _employeeRepository.UpdateAsync(employee);
        }

        public async Task AssignToPositionAsync(int employeeId, int positionId)
        {
            var position = await _positionRepository.GetByIdAsync(positionId);
            if (position == null)
                throw new ArgumentException("Position not found");

            var employee = await _employeeRepository.GetByIdAsync(employeeId);
            employee.PositionId = positionId;
            await _employeeRepository.UpdateAsync(employee);
        }
    }
}
