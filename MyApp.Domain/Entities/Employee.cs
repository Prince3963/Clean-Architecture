using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyApp.Domain.Entities
{
    public class Employee
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string? LastName { get; set; }
        public string Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string Password { get; set; } 
        public int Salary { get; set; }
        public bool IsDelete { get; set; } = false;
    }
}
