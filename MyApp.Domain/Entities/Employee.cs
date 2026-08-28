using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace MyApp.Domain.Entities
{
    public class Employee : GenericEntity
    {
        public string FirstName { get; set; }
        public string? LastName { get; set; }
        public string Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string Password { get; set; } 
        public int Salary { get; set; }

        public int? DepartmentId { get; set; }
        public Department Department { get; set; }
    }
}
