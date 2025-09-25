using Company.DAL.Data.Contexts;
using Company.DAL.Repositories.Interface;



namespace Company.DAL.Repositories.UnitOfWorks
{
    public class UnitOfWork(IDepartmentRepositories _departmentRepositories, IEmployeeRepositories _employeeRepositories,ApplicationDbContext _context) : IUnitOfWork
    {
        public IEmployeeRepositories EmployeeRepositories => _employeeRepositories;

        public IDepartmentRepositories DepartmentRepositories => _departmentRepositories;

        public int SaveChanges() => _context.SaveChanges();
      
    }
}
