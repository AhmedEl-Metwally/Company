using Company.DAL.Data.Contexts;
using Company.DAL.Repositories.Interface;

namespace Company.DAL.Repositories.Services
{
    public class DepartmentRepositories(ApplicationDbContext _context) : IDepartmentRepositories
    {
        //GetAll
        public IEnumerable<Department> GetAll(bool withTracking = false)
        {
            if (withTracking)
                return _context.Departments.ToList();
            else
                return _context.Departments.AsNoTracking().ToList();

        }

        //GetById
        public Department GetById(int id) => _context.Departments.Find(id) ?? throw new KeyNotFoundException($"Department with id {id} not found");

        //Add
        public int Add(Department department)
        {
            if (department == null)
                throw new ArgumentNullException(nameof(department), "Department cannot be null");

            if (string.IsNullOrWhiteSpace(department.Name))
                throw new ArgumentException(nameof(department), "Department name is required.");

            _context.Departments.Add(department);
            return _context.SaveChanges();
        }

        //Update
        public int Update(Department department)
        {
            ArgumentNullException.ThrowIfNull(department);

            if (department.Id <= 0 || !_context.Departments.Any(d => d.Id == department.Id))
                throw department.Id <= 0 ? new ArgumentException("Invalid Department Id.", nameof(department))
                                         : new KeyNotFoundException($"Department with Id {department.Id} not found.");

            _context.Departments.Update(department);
            return _context.SaveChanges();
        }

        //Remove
        public int Remive(Department department)
        {
            ArgumentNullException.ThrowIfNull(department);

            if (department.Id <= 0 || !_context.Departments.Any(d => d.Id == department.Id))
                throw department.Id <= 0 ? new ArgumentException("Invalid Department Id.", nameof(department))
                                        : new KeyNotFoundException($"Department with Id {department.Id} not found.");

            _context.Departments.Remove(department);
            return _context.SaveChanges();
        }

    }
}
