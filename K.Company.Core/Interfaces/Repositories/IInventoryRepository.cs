using K.Company.Core.DAOs;

namespace K.Company.Core.Interfaces.Repositories
{
    public interface IInventoryRepository : IBaseRepository<Inventory>
    {
        Task<Inventory> GetByProductId(long productId);
        IEnumerable<Inventory> GetDetailInvetory();
    }
}
