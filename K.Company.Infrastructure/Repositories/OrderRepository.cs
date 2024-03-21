using K.Company.Core.DAOs;
using K.Company.Core.Interfaces.Repositories;
using K.Company.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace K.Company.Infrastructure.Repositories
{
    public class OrderRepository : BaseRepository<Order>, IOrderRepository
    {
        public OrderRepository(KCompDBContext context): base(context)
        {
            
        }

        public IEnumerable<Order> GetOrders()
        {
            return _ctx.Orders
                .Include(x => x.Customer)
                .Include(x => x.OrderItems)
                .ThenInclude(x => x.Product)
                .AsEnumerable();
        }
    }
}
