using RestarauntSystem.Core.Models;


namespace RestarauntSystem.Core.Services
{
    public interface IEmployeeService
    {
        Task<IEnumerable<Employee>> GetAllEmployeesAsync();
        Task<Employee> GetEmployeeDetailsAsync(int employeeId);
        Task<Employee> CreateEmployeeAsync(Employee employee);
        Task UpdateEmployeeAsync(Employee employee);
        Task AssignToPositionAsync(int employeeId, int positionId);
    }
}
