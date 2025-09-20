using Company.BLL.Dtos;
using Company.BLL.Factory;
using Company.BLL.Repositories.Interface;
using Company.DAL.Models;
using Company.DAL.Repositories.Interface;
using Company.DAL.Repositories.Services;

namespace Company.BLL.Repositories.Services
{
    public class DepartmentService(IDepartmentRepositories _departmentRepositories) : IDepartmentService
    {
        //GetAll
        public IEnumerable<DepartmentDto> GetAllDepartments()
        {
            var department = _departmentRepositories.GetAll();
            return department.Select(d => d.ToDepartmentDto());
        }

        //GetById
        public DepartmentDetailsDto? GetDepartmentById(int id)
        {
            var department = _departmentRepositories.GetById(id);
            return department is null ? null : department.ToDepartmentDetailsDto();
        }

        //Add
        public int AddDepartment(CreateDepartmentDto departmentDto) => _departmentRepositories.Add(departmentDto.ToDepartment());

        //Update
        public int UpdatedDepartment(UpdatedDepartmentDto updateddepartmentDto) => _departmentRepositories.Update(updateddepartmentDto.ToDepartment());

        //Delete
        public bool DeleteDepartment(int id)
        {
            var department = _departmentRepositories.GetById(id);

            if (department is null)
                return false;
            int numOfRows = _departmentRepositories.Remive(department);
            return numOfRows > 0 ? true : false;
        }

    }
}




