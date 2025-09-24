using Company.DAL.Models.EmployeeModule;
using Company.DAL.Models.Shared;

namespace Company.DAL.Models.DepartmentModule
{
    public class Department :BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public string? Description { get; set; }

        public ICollection<Employee> Employees { get; set; } = new HashSet<Employee>();
    }
}
