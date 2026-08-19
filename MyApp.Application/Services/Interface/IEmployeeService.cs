using MyApp.Application.DTOs;
using MyApp.Domain.Entities;
using MyApp.Domain.HelperServices;

namespace MyApp.Application.Services.Interface
{
    public interface IEmployeeService
    {
        public Task<ServiceResponse<List<Employee>>> GetEmployee();
        public Task<ServiceResponse<AddEmployeeDTO>> AddEmployee(AddEmployeeDTO employee);
        public Task<ServiceResponse<Employee>> DeleteEmployee(int id);
        public Task<ServiceResponse<Employee>> UpdateEmployee(AddEmployeeDTO employee, int id);
    }
}
