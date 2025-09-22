namespace Company.BLL.Dtos.DepartmentDtos
{
    public class DepartmentDetailsDto
    {
        public int Id { get; set; }
        public int CreateBy { get; set; }
        public DateOnly? CreateOn { get; set; }
        public int ModifiedBy { get; set; }
        public DateOnly? ModifiedOn { get; set; }
        public bool IsDeleted { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public string? Description { get; set; }
    }
}
