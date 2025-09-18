using Company.DAL.Models;
using Company.DAL.Repositories;

namespace Company.BLL.Services
{
    public class DepartmentService(DepartmentRepositories _departmentRepositories)
    {
        public Department GetById(int id) => _departmentRepositories.GetById(id);
      
    }
}




