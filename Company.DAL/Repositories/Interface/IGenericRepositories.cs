using Company.DAL.Models.Shared;

namespace Company.DAL.Repositories.Interface
{
    public interface IGenericRepositories<T> where T :BaseEntity
    {
        int Add(T entity);
        IEnumerable<T> GetAll(bool withTracking = false);
        T? GetById(int id);
        int Remove(T entity);
        int Update(T entity);
    }
}
