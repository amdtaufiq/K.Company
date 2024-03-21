using K.Company.Core.CustomEntities;
using K.Company.Core.DAOs;
using K.Company.Core.DTOs;
using K.Company.Core.Filters;

namespace K.Company.Core.Interfaces.Services
{
    public interface IOrderService
    {
        Task<bool> AddOrder(Order order, Sales sales);
        Task<bool> UpdateOrderStatus(OrderStatusRequest orderStatus);
        PagedList<Sales> GetSalesDetail(OrderFilter filters);
        PagedList<Order> GetOrders(OrderFilter filters);
    }
}
