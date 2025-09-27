namespace Company.PL.ViewModels
{
    public class CreateEditDepartmentViewModels
    {
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; } 
        public DateOnly CreatedOn { get; set; }
    }
}
