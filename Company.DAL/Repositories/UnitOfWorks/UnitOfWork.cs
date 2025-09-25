using Company.DAL.Data.Contexts;
using Company.DAL.Repositories.Implementation;
using Company.DAL.Repositories.Interface;



namespace Company.DAL.Repositories.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private readonly Lazy<IEmployeeRepositories> _employeeRepositories;
        private readonly Lazy<IDepartmentRepositories> _departmentRepositories;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            _employeeRepositories = new Lazy<IEmployeeRepositories>(()=> new EmployeeRepositories(_context));
            _departmentRepositories = new Lazy<IDepartmentRepositories>(()=> new DepartmentRepositories(_context));
        }

        public IEmployeeRepositories EmployeeRepositories => _employeeRepositories.Value;

        public IDepartmentRepositories DepartmentRepositories => _departmentRepositories.Value;

        public int SaveChanges() => _context.SaveChanges();
      
    }
}
