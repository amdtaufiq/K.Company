using K.Company.Core.DAOs;
using K.Company.Core.Interfaces.Repositories;

namespace K.Company.Core.Interfaces.Unit
{
    public interface IUnitOfWork
    {
        IBaseRepository<Customer> CustomerRepository { get; }
        IInventoryRepository InventoryRepository { get; }
        IOrderRepository OrderRepository { get; }
        IBaseRepository<OrderItem> OrderItemRepository { get; }
        ISalesRepository SalesRepository { get; }
        IProductRepository ProductRepository { get; }
        IStoreRepository StoreRepository { get; }
        void SaveChanges();
        Task SaveChangesAsync();
    }
}
