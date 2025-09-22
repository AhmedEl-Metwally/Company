using Company.BLL.Dtos.DepartmentDtos;

namespace Company.BLL.Repositories.Interface
{
    public interface IDepartmentService
    {
        int AddDepartment(CreateDepartmentDto departmentDto);
        bool DeleteDepartment(int id);
        IEnumerable<DepartmentDto> GetAllDepartments();
        DepartmentDetailsDto? GetDepartmentById(int id);
        int UpdatedDepartment(UpdatedDepartmentDto updateddepartmentDto);
    }
}