using Company.DAL.Data.Contexts;
using Company.DAL.Models.DepartmentModule;
using Company.DAL.Repositories.Interface;

namespace Company.DAL.Repositories.Services
{
    public class DepartmentRepositories(ApplicationDbContext _context) : GenericRepositories<Department>(_context) , IDepartmentRepositories
    {
    }
}
