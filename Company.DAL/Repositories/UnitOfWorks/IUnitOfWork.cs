using Company.DAL.Repositories.Interface;


namespace Company.DAL.Repositories.UnitOfWorks
{
    public interface IUnitOfWork
    {
        public IEmployeeRepositories EmployeeRepositories  { get; }
        public IDepartmentRepositories DepartmentRepositories  { get; }
        public int SaveChanges();
    }
}
