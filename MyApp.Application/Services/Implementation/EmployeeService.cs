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
        private readonly IDepartmentRepo departmentRepo;

        public EmployeeService(IEmployeeRepo employeeRepo, IDepartmentRepo departmentRepo   )
        {
            this.employeeRepo = employeeRepo;
            this.departmentRepo = departmentRepo;
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
                var existDepartment = await departmentRepo.GetDepartmentsByName(employee.DepartmentName);

                if (existDepartment == null)
                {
                    response.Success = false;
                    response.Message = "Department is not exist";
                    response.StatusCode = HttpStatusCode.NotFound;

                    return response;
                }
                var newEmployee = new Employee
                {
                    FirstName = employee.FirstName,
                    LastName = employee.LastName,
                    Email = employee.Email,
                    PhoneNumber = employee.PhoneNumber,
                    Password = employee.Password,
                    Salary = employee.Salary,
                    DepartmentId = existDepartment.Id,
                    CreatedBy = existDepartment.Name
                };

                var result = await employeeRepo.AddEmployee(newEmployee);

                response.Data = new AddEmployeeDTO
                {
                    FirstName = result.FirstName,
                    LastName = result.LastName,
                    Email = result.Email,
                    PhoneNumber = result.PhoneNumber,
                    Password = result.Password,
                    Salary = result.Salary,
                    DepartmentName = result.DepartmentId.ToString()
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

        public async Task<DeleteServiceResponse<bool>> BulkDelete(List<int> ids)
        {
            var response = new DeleteServiceResponse<bool>();
            try
            {
                var result = await employeeRepo.BulkDelete(ids);
                response.Success = true;
                response.Message = "Employees Deleted Successfully";
                return response;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Error = ex.Message;
                return response;
            }
        }

        public async Task<DeleteServiceResponse<bool>> DeleteEmployee(int id)
        {
            var response = new DeleteServiceResponse<bool>();
            try
            {

                var result = await employeeRepo.DeleteEmployee(id);

                response.Success = true;
                response.Message = "Employee Deleted Successfully";

                return response;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Error = ex.Message;

                return response;
            }
        }

        public async Task<ServiceResponse<List<GetEmployeeDTO>>> GetEmployee()
        {
            var response = new ServiceResponse<List<GetEmployeeDTO>>();
            try
            {
                var result = await employeeRepo.GetEmployee();

                response.Data = result.Select(e => new GetEmployeeDTO
                {
                    FirstName = e.FirstName,
                    LastName = e.LastName,
                    Email = e.Email,
                    PhoneNumber = e.PhoneNumber,
                    Password = e.Password,
                    Salary = e.Salary,
                    IsDelete = e.IsDelete,
                    CreatedBy = e.CreatedBy,
                    CreatedAt = e.CreatedAt,
                    Department = e.Department == null ? null : new GetDepartmentDTO
                    {
                        Name = e.Department.Name,
                        Description = e.Department.Description,
                        CreatedBy = e.Department.CreatedBy,
                        CreatedAt = e.Department.CreatedAt
                    }
                }).ToList();
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

        public async Task<ServiceResponse<GetEmployeeDTO>> GetEmployeeByName(string name)
        {
            var response = new ServiceResponse<GetEmployeeDTO>();
            try
            {
                var existEmployee = await employeeRepo.GetEmployeeByName(name);
                if (existEmployee == null)
                {
                    response.Success = false;
                    response.Message = "Employee not found";
                    response.StatusCode = HttpStatusCode.NotFound;

                    return response;
                }


                response.Data = new GetEmployeeDTO
                {
                    FirstName = existEmployee.FirstName,
                    LastName = existEmployee.LastName,
                    Email = existEmployee.Email,
                    PhoneNumber = existEmployee.PhoneNumber,
                    Password = existEmployee.Password,
                    Salary = existEmployee.Salary,
                    IsDelete = existEmployee.IsDelete,
                    CreatedBy = existEmployee.CreatedBy,
                    CreatedAt = existEmployee.CreatedAt,
                    Department = existEmployee.Department == null ? null : new GetDepartmentDTO
                    {
                        Name = existEmployee.Department.Name,
                        Description = existEmployee.Department.Description,
                        CreatedBy = existEmployee.Department.CreatedBy,
                        CreatedAt = existEmployee.Department.CreatedAt
                    }
                };
                response.Success = true;
                response.Message = "Employee Retrieved Successfully By it's Name";
            }
            catch
            {
                response.Success = false;
                response.Message = "Failed to retrieve employee by name";
                response.Error = "An error occurred while retrieving the employee by name.";
            }

            return response;
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
