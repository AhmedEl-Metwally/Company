using Company.DAL.Data.Contexts;
using Company.DAL.Models.Shared;
using Company.DAL.Repositories.Interface;

namespace Company.DAL.Repositories.Implementation
{
    public class GenericRepositories<T>(ApplicationDbContext _context) : IGenericRepositories<T> where T : BaseEntity
    {
        public IEnumerable<T> GetAll(bool withTracking = false)
        {
            if (withTracking)
                return _context.Set<T>().Where(entity => entity.IsDeleted == false).ToList();
            else
                return _context.Set<T>().Where(entity => entity.IsDeleted == false).AsNoTracking().ToList();

        }

        //GetById
        public T? GetById(int id) => _context.Set<T>().Find(id) ?? throw new KeyNotFoundException($"Department with id {id} not found");

        //Add
        public int Add(T entity)
        {
            _context.Set<T>().Add(entity);
            return _context.SaveChanges();
        }

        //Update
        public int Update(T entity)
        {
            _context.Set<T>().Update(entity);
            return _context.SaveChanges();
        }

        //Remove
        public int Remove(T entity)
        {
            _context.Set<T>().Remove(entity);
            return _context.SaveChanges();
        }
    }
}
