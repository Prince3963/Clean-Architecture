using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyApp.Domain.Entities;

namespace MyApp.Application.RepoInterface
{
    public interface IDepartmentRepo
    {
        Task<List<Department>> GetDepartments();
        Task<Department?> GetDepartmentsByName(string name);
        Task<Department?> AddDepartment(Department department);
    }
}
