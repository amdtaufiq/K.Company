using K.Company.Core.CustomEntities;
using K.Company.Core.DAOs;
using K.Company.Core.Filters;

namespace K.Company.Core.Interfaces.Services
{
    public interface IStoreService
    {
        PagedList<Store> GetStoreList(StoreFilter filters);
        Task<Store> GeStoreById(string storeCode);
        Task<bool> AddStore(Store store);
        Task<bool> UpdateStore(string storeCode, Store store);
        Task<bool> DeleteStore(string storeCode);
    }
}
