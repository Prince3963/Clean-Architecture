using Microsoft.EntityFrameworkCore;
using MyApp.Application.RepoInterface;
using MyApp.Domain.Entities;
using MyApp.Infrastrcture.Data;

namespace MyApp.Infrastrcture.RepoImplementation
{
    public class DepartmentRepo : IDepartmentRepo
    {
        private readonly AppDbContext dbContext;
        public DepartmentRepo(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Department?> AddDepartment(Department department)
        {
            var result = await dbContext.Departments.AddAsync(department);
            await dbContext.SaveChangesAsync();
            return department;
        }

        public async Task<List<Department>> GetDepartments()
        {
            return await dbContext.Departments.ToListAsync();
        }

        public async Task<Department?> GetDepartmentsByName(string name)
        {
            var existDepartment =  await dbContext.Departments.FirstOrDefaultAsync(d => d.Name == name);
            if (existDepartment == null) return null;
            return existDepartment;
        }
    }
}
