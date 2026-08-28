using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyApp.Application.DTOs;
using MyApp.Domain.Entities;
using MyApp.Domain.HelperServices;

namespace MyApp.Application.Services.Interface
{
    public interface IDepartmentService
    {
        Task<ServiceResponse<List<Department>>> GetDepartments();
        Task<ServiceResponse<AddDepartmentDTO>> AddDepartment(AddDepartmentDTO department);
    }
}
