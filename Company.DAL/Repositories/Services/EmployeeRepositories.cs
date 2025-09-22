using Company.DAL.Data.Contexts;
using Company.DAL.Models.EmployeeModule;
using Company.DAL.Repositories.Interface;

namespace Company.DAL.Repositories.Services
{
    public class EmployeeRepositories(ApplicationDbContext _context) : GenericRepositories<Employee>(_context) ,IEmployeeRepositories
    {
    }
}
