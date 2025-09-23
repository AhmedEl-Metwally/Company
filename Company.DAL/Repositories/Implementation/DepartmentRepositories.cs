using Company.DAL.Data.Contexts;
using Company.DAL.Models.DepartmentModule;
using Company.DAL.Repositories.Interface;

namespace Company.DAL.Repositories.Implementation
{
    public class DepartmentRepositories(ApplicationDbContext _context) : GenericRepositories<Department>(_context) , IDepartmentRepositories
    {
    }
}
