namespace Company.DAL.Repositories.Interface
{
    public interface IDepartmentRepositories
    {
        int Add(Department department);
        IEnumerable<Department> GetAll(bool withTracking = false);
        Department GetById(int id);
        int Remive(Department department);
        int Update(Department department);
    }
}