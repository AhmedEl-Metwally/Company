using Company.BLL.Dtos;
using Company.DAL.Models;

namespace Company.BLL.Factory
{
    public static class DepartmentFactory
    {
        public static DepartmentDto ToDepartmentDto(this Department department)
        {
            return new DepartmentDto()
            {
                DeptId = department.Id,
                Code = department.Code,
                Name = department.Name,
                Description = department.Description,
                DateOfCreation = department.CreateOn.HasValue ? DateOnly.FromDateTime(department.CreateOn.Value) : default

            }; 
        }

        public static DepartmentDetailsDto ToDepartmentDetailsDto(this Department department)
        {
            return new DepartmentDetailsDto()
            {
                Id = department.Id,
                Code = department.Code,
                Name = department.Name,
                Description = department.Description,
                CreateBy = department.CreateBy,
                IsDeleted = department.IsDeleted,
                ModifiedBy = department.ModifiedBy,
                CreateOn = department.CreateOn.HasValue ? DateOnly.FromDateTime(department.CreateOn.Value) : default,
                ModifiedOn = department.ModifiedOn.HasValue ? DateOnly.FromDateTime(department.ModifiedOn.Value) : default
            };
        }

        public static Department ToDepartment(this CreateDepartmentDto departmentDto) 
        {
            return new Department() 
            {
                Description = departmentDto.Description,
                Name = departmentDto.Name,
                Code = departmentDto.Code,
                CreateOn =departmentDto.DateOfCreation.ToDateTime(new TimeOnly())

            };
        }

        public static Department ToDepartment(this UpdatedDepartmentDto departmentDto)
        {
            return new Department()
            {
                Id = departmentDto.Id,  
                Description = departmentDto.Description,
                Name = departmentDto.Name,
                Code = departmentDto.Code,
                CreateOn = departmentDto.DateOfCreation.ToDateTime(new TimeOnly())

            };
        }

    }
}
