using MyApp.Application.DTOs;
using MyApp.Application.RepoInterface;
using MyApp.Application.Services.Interface;
using MyApp.Domain.Entities;
using MyApp.Domain.HelperServices;

namespace MyApp.Application.Services.Implementation
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepo departmentRepo;
        private readonly IEmployeeRepo employeeRepo;
        public DepartmentService(IDepartmentRepo departmentRepo, IEmployeeRepo employeeRepo)
        {
            this.departmentRepo = departmentRepo;
            this.employeeRepo = employeeRepo;
        }

        public async Task<ServiceResponse<AddDepartmentDTO>> AddDepartment(AddDepartmentDTO department)
        {
            var response = new ServiceResponse<AddDepartmentDTO>();
            try
            {
                var existingDepartment = await departmentRepo.GetDepartmentsByName(department.Name);
                if (existingDepartment != null)
                {
                    response.Success = false;
                    response.Message = "Department already exists";
                    response.StatusCode = System.Net.HttpStatusCode.Conflict;
                    return response;
                }

                var newDepartment = new Department
                {
                    Name = department.Name,
                    Description = department.Description ?? string.Empty,
                    CreatedAt = DateTime.UtcNow,
                };

                var result = await departmentRepo.AddDepartment(newDepartment);
                if (result == null)
                {
                    response.Success = false;
                    response.Message = "Failed to add department";
                    response.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                    return response;
                }

                response.Data = new AddDepartmentDTO
                {
                    Id = result.Id,
                    Name = result.Name,
                    Description = result.Description
                };
                response.Success = true;
                response.Message = "Department added successfully";
                response.StatusCode = System.Net.HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Failed to add department";
                response.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                response.Error = ex.InnerException?.Message ?? ex.Message;
            }
            return response;
        }

        public async Task<ServiceResponse<List<Department>>> GetDepartments()
        {
            var response = new ServiceResponse<List<Department>>();
            try
            {
                var result = await departmentRepo.GetDepartments();
                response.Data = result;
                response.StatusCode = System.Net.HttpStatusCode.OK;
                response.Success = true;
                response.Message = "Departments retrieved successfully";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Failed to get departments";
                response.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                response.Error = ex.Message;
            }
            return response;
        }
    }
}
