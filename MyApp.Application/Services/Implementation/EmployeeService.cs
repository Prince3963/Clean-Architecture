using MyApp.Application.DTOs;
using MyApp.Application.RepoInterface;
using MyApp.Application.Services.Interface;
using MyApp.Domain.Entities;
using MyApp.Domain.HelperServices;
using System.Net;

namespace MyApp.Application.Services.Implementation
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepo employeeRepo;

        public EmployeeService(IEmployeeRepo employeeRepo)
        {
            this.employeeRepo = employeeRepo;
        }

        public async Task<ServiceResponse<AddEmployeeDTO>> AddEmployee(AddEmployeeDTO employee)
        {
            var response = new ServiceResponse<AddEmployeeDTO>();
            try
            {
                var existEmail = await employeeRepo.GetEmployeeByEmail(employee.Email);
                if (existEmail != null)
                {
                    response.Success = false;
                    response.Message = "Email already exist";
                    response.StatusCode = HttpStatusCode.Conflict;

                    return response;
                }
                var newEmployee = new Employee
                {
                    FirstName = employee.FirstName,
                    LastName = employee.LastName,
                    Email = employee.Email,
                    PhoneNumber = employee.PhoneNumber,
                    Password = employee.Password,
                    Salary = employee.Salary
                };

                var result = await employeeRepo.AddEmployee(newEmployee);

                response.Data = new AddEmployeeDTO
                {
                    FirstName = result.FirstName,
                    LastName = result.LastName,
                    Email = result.Email,
                    PhoneNumber = result.PhoneNumber,
                    Password = result.Password,
                    Salary = result.Salary
                };
                response.Success = true;
                response.Message = "Employee Register Successfully";
                response.StatusCode = HttpStatusCode.OK;

                return response;

            }catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Employee not registerd";
                response.Error = ex.Message;

                return response;
            }
        }

        public async Task<ServiceResponse<Employee>> DeleteEmployee(int id)
        {
            var response = new ServiceResponse<Employee>();
            try
            {

                var result = await employeeRepo.DeleteEmployee(id);

                response.Data = result;
                response.Success = true;
                response.Message = "Employee Deleted Successfully";
                response.StatusCode = HttpStatusCode.OK;

                return response;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Employee not registerd";
                response.Error = ex.Message;

                return response;
            }
        }

        public async Task<ServiceResponse<List<Employee>>> GetEmployee()
        {
            var response = new ServiceResponse<List<Employee>>();
            try
            {
                var result = await employeeRepo.GetEmployee();

                response.Data = result;
                response.Success = true;
                response.Message = "Employees Retrieved Successfully";
                response.StatusCode = HttpStatusCode.OK;

                return response;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Failed to retrieve employees";
                response.Error = ex.Message;

                return response;
            }
        }

        public async Task<ServiceResponse<Employee>> UpdateEmployee(AddEmployeeDTO employee, int id)
        {
            var response = new ServiceResponse<Employee>();
            try
            {
                var updatedEmployee = new Employee
                {
                    FirstName = employee.FirstName,
                    LastName = employee.LastName,
                    Email = employee.Email,
                    PhoneNumber = employee.PhoneNumber,
                    Password = employee.Password,
                    Salary = employee.Salary
                };

                var result = await employeeRepo.UpdateEmployee(updatedEmployee, id);
                response.Data = result;
                response.Success = true;
                response.Message = "Employee Updated Successfully";
                response.StatusCode = HttpStatusCode.OK;

                return response;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Failed to update employee";
                response.Error = ex.Message;

                return response;
            }
        }
    }
}
