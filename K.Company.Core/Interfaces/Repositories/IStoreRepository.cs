using K.Company.Core.DAOs;

namespace K.Company.Core.Interfaces.Repositories
{
    public interface IStoreRepository : IBaseRepository<Store>
    {
        Task<Store> GetByCode(string storeCode);
    }
}
