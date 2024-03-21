using K.Company.Core.DAOs;

namespace K.Company.Core.Interfaces.Repositories
{
    public interface IOrderRepository : IBaseRepository<Order>
    {
        IEnumerable<Order> GetOrders();
    }
}
