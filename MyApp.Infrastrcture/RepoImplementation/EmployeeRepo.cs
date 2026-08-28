using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyApp.Application.RepoInterface;
using MyApp.Domain.Entities;
using MyApp.Infrastrcture.Data;

namespace MyApp.Infrastrcture.RepoImplementation
{
    public class EmployeeRepo : IEmployeeRepo
    {
        AppDbContext dbContext;
        public EmployeeRepo(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<Employee> AddEmployee(Employee employee)
        {
            await dbContext.Employees.AddAsync(employee);
            await dbContext.SaveChangesAsync();
            return employee;
        }

        public async Task<bool> BulkDelete(List<int> ids)
        {
            var empIds = await dbContext.Employees.Where(e => ids.Contains(e.Id) && e.IsDelete == false).ToListAsync();
            
            if (empIds == null)
            {
                throw new Exception("Employee are not found");
            }

            foreach(var emp in empIds)
            {
                emp.IsDelete = true;
            }
            await dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteEmployee(int id)
        {
            var empId = await dbContext.Employees.FirstOrDefaultAsync(e => e.Id == id);
            if (empId == null || empId.IsDelete == true)
            {
                throw new Exception("Employee not found or already deleted");
            }
            empId.IsDelete = true;
            await dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<List<Employee>> GetEmployee()
        {
            return await dbContext.Employees.ToListAsync();
        }

        public async Task<Employee> GetEmployeeByEmail(string email)
        {
            var existingEmail = await dbContext.Employees.FirstOrDefaultAsync(e => e.Email == email);
            if (existingEmail == null) return null;

            return existingEmail;
        }

        public async Task<Employee> UpdateEmployee(Employee employee, int id)
        {
            var existingId = dbContext.Employees.FirstOrDefault(e => e.Id == id);

            if (existingId == null)
            {
                return null;
            }

            existingId.FirstName = employee.FirstName;
            existingId.LastName = employee.LastName;
            existingId.Email = employee.Email;
            existingId.PhoneNumber = employee.PhoneNumber;

            await dbContext.SaveChangesAsync();
            return existingId;
        }
    }
}
