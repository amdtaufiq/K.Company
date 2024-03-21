using K.Company.Core.DAOs;
using K.Company.Core.Interfaces.Repositories;
using K.Company.Core.Interfaces.Unit;
using K.Company.Infrastructure.Data;
using K.Company.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace K.Company.Infrastructure.Unit
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly KCompDBContext _ctx;
        private readonly IBaseRepository<Customer> _customerRepository;
        private readonly IInventoryRepository _inventoryRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IBaseRepository<OrderItem> _orderItemRepository;
        private readonly ISalesRepository _salesRepository;
        private readonly IProductRepository _productRepository;
        private readonly IStoreRepository _storeRepository;

        public UnitOfWork(KCompDBContext ctx)
        {
            _ctx = ctx;
        }

        public IBaseRepository<Customer> CustomerRepository => _customerRepository ?? new BaseRepository<Customer>(_ctx);

        public IInventoryRepository InventoryRepository => _inventoryRepository ?? new InventoryRepository(_ctx);

        public IOrderRepository OrderRepository => _orderRepository ?? new OrderRepository(_ctx);

        public IBaseRepository<OrderItem> OrderItemRepository => _orderItemRepository ?? new BaseRepository<OrderItem>(_ctx);

        public ISalesRepository SalesRepository => _salesRepository ?? new SalesRepository(_ctx);

        public IProductRepository ProductRepository => _productRepository ?? new ProductRepository(_ctx);

        public IStoreRepository StoreRepository => _storeRepository ?? new StoreRepository(_ctx);


        public void SaveChanges()
        {
            var entries = _ctx.ChangeTracker
                .Entries()
                .Where(e => e.Entity is BaseEntity && (
                        e.State == EntityState.Added
                        || e.State == EntityState.Modified));

            foreach (var entityEntry in entries)
            {

                if (entityEntry.State == EntityState.Added)
                {
                    ((BaseEntity)entityEntry.Entity).CreatedAt = DateTime.UtcNow;
                }
                else
                {
                    ((BaseEntity)entityEntry.Entity).UpdatedAt = DateTime.UtcNow;
                }
            }

            _ctx.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            var entries = _ctx.ChangeTracker
                .Entries()
                .Where(e => e.Entity is BaseEntity && (
                        e.State == EntityState.Added
                        || e.State == EntityState.Modified));

            foreach (var entityEntry in entries)
            {

                if (entityEntry.State == EntityState.Added)
                {
                    ((BaseEntity)entityEntry.Entity).CreatedAt = DateTime.UtcNow;
                }
                else
                {
                    ((BaseEntity)entityEntry.Entity).UpdatedAt = DateTime.UtcNow;
                }
            }

            await _ctx.SaveChangesAsync();
        }
    }
}
