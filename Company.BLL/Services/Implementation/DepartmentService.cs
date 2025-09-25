using Company.BLL.Dtos.DepartmentDtos;
using Company.BLL.Factory;
using Company.BLL.Services.Interface;
using Company.DAL.Repositories.UnitOfWorks;

namespace Company.BLL.Services.Implementation
{
    public class DepartmentService(IUnitOfWork _unitOfWork) : IDepartmentService
    {
        //GetAll
        public IEnumerable<DepartmentDto> GetAllDepartments()
        {
            var department =_unitOfWork.DepartmentRepositories.GetAll();
            return department.Select(d => d.ToDepartmentDto());
        }

        //GetById
        public DepartmentDetailsDto? GetDepartmentById(int id)
        {
            var department = _unitOfWork.DepartmentRepositories.GetById(id);
            return department is null ? null : department.ToDepartmentDetailsDto();
        }

        //Add
        public int AddDepartment(CreateDepartmentDto departmentDto) 
         {
            _unitOfWork.DepartmentRepositories.Add(departmentDto.ToDepartment());
            return _unitOfWork.SaveChanges();
        }

        //Update
        public int UpdatedDepartment(UpdatedDepartmentDto updateddepartmentDto) 
        {
            _unitOfWork.DepartmentRepositories.Update(updateddepartmentDto.ToDepartment());
            return _unitOfWork.SaveChanges();
        }

        //Delete
        public bool DeleteDepartment(int id)
        {
            var department = _unitOfWork.DepartmentRepositories.GetById(id);
            if (department is null)
                return false;
            _unitOfWork.DepartmentRepositories.Remove(department);
            int numOfRows = _unitOfWork.SaveChanges();
            return numOfRows > 0 ? true : false;
        }

    }
}




