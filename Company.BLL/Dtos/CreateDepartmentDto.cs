using System.ComponentModel.DataAnnotations;

namespace Company.BLL.Dtos
{
    public class CreateDepartmentDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public string Code { get; set; } = string.Empty;
        public string? Description { get; set; }
        public DateOnly DateOfCreation { get; set; }
    }
}
