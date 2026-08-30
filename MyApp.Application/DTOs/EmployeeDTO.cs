using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Application.DTOs
{
    public class AddEmployeeDTO
    {
        public string FirstName { get; set; }
        public string? LastName { get; set; }

        public string Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string Password { get; set; }
        public int Salary { get; set; }
        public string DepartmentName { get; set; }
    }

    public class LoginEmployeeDTO
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class GetEmployeeDTO
    {
        public string FirstName { get; set; }
        public string? LastName { get; set; }
        public string Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string Password { get; set; }
        public int Salary { get; set; }
        public bool IsDelete { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;
        public GetDepartmentDTO Department { get; set; }
    }
}
