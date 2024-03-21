using K.Company.Core.DAOs;
using K.Company.Core.Interfaces.Repositories;
using K.Company.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace K.Company.Infrastructure.Repositories
{
    public class StoreRepository : BaseRepository<Store>, IStoreRepository
    {
        public StoreRepository(KCompDBContext context) : base(context)
        {

        }

        public async Task<Store> GetByCode(string storeCode)
        {
            return await _ctx.Stores.Where(x => x.StoreCode == storeCode).FirstOrDefaultAsync();
        }
    }
}
