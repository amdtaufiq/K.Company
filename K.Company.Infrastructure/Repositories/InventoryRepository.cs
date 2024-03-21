using K.Company.Core.DAOs;
using K.Company.Core.Interfaces.Repositories;
using K.Company.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace K.Company.Infrastructure.Repositories
{
    public class InventoryRepository : BaseRepository<Inventory>, IInventoryRepository
    {
        public InventoryRepository(KCompDBContext context) : base(context)
        {

        }

        public async Task<Inventory> GetByProductId(long productId)
        {
            return await _ctx.Inventories.Where(x => x.ProductId == productId).FirstOrDefaultAsync();
        }

        public IEnumerable<Inventory> GetDetailInvetory()
        {
            return _ctx.Inventories
                .Include(x => x.Product)
                .AsEnumerable();
        }
    }
}
