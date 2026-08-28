using MyApp.Domain.Entities;

namespace MyApp.Application.RepoInterface
{
    public interface IEmployeeRepo
    {
        Task<List<Employee>> GetEmployee();
        Task<Employee> AddEmployee(Employee employee);
        Task<bool> DeleteEmployee(int id);
        Task<Employee> UpdateEmployee(Employee employee, int id);
        Task<Employee> GetEmployeeByEmail(string email);
        Task<bool> BulkDelete(List<int> ids);
    }
}
