using Company.DAL.Models.Shared;
using System.Linq.Expressions;

namespace Company.DAL.Repositories.Interface
{
    public interface IGenericRepositories<T> where T :BaseEntity
    {
        int Add(T entity);
        IEnumerable<T> GetAll(bool withTracking = false);
        IEnumerable<T> GetAll(Expression<Func<T,bool>> expression);
        T? GetById(int id);
        int Remove(T entity);
        int Update(T entity);
    }
}
