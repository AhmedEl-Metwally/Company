using Company.DAL.Models.Shared;
using System.Linq.Expressions;

namespace Company.DAL.Repositories.Interface
{
    public interface IGenericRepositories<T> where T :BaseEntity
    {
        void Add(T entity);
        IEnumerable<T> GetAll(bool withTracking = false);
        IEnumerable<T> GetAll(Expression<Func<T,bool>> expression);
        T? GetById(int id);
        void Remove(T entity);
        void Update(T entity);
    }
}
