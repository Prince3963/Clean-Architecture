using System.Text.Json.Serialization;

namespace MyApp.Domain.Entities
{
    public class Department : GenericEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }

        [JsonIgnore]
        public ICollection<Employee> Employees { get; set; } = new List<Employee>();
    }
}
