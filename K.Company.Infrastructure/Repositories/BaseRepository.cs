using K.Company.Core.DAOs;
using K.Company.Core.Interfaces.Repositories;
using K.Company.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace K.Company.Infrastructure.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        protected readonly KCompDBContext _ctx;
        protected readonly DbSet<T> _entities;

        public BaseRepository(KCompDBContext ctx)
        {
            _ctx = ctx;
            _entities = ctx.Set<T>();
        }

        public async Task Add(T entity)
        {
            await _entities.AddAsync(entity);
        }

        public void Delete(T entity)
        {
            _entities.Update(entity);
        }

        public IEnumerable<T> GetAll()
        {
            return _entities
                .AsEnumerable();
        }

        public async Task<T> GetById(long id)
        {
            return await _entities
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public void Update(T entity)
        {
            _entities.Update(entity);
        }
    }
}
