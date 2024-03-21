using K.Company.Core.DAOs;

namespace K.Company.Core.Interfaces.Repositories
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        IEnumerable<T> GetAll();
        Task<T> GetById(long id);
        Task Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
