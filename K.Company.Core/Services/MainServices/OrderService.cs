using K.Company.Core.CustomEntities;
using K.Company.Core.DAOs;
using K.Company.Core.DTOs;
using K.Company.Core.Exceptions;
using K.Company.Core.Filters;
using K.Company.Core.Interfaces.Services;
using K.Company.Core.Interfaces.Unit;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace K.Company.Core.Services.MainServices
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unit;
        private readonly ILogger _logger;
        private readonly PaginationOptions _paginationOptions;


        public OrderService(
            IUnitOfWork unit,
            ILoggerFactory loggerFactory,
            IOptions<PaginationOptions> paginationOptions)
        {
            _unit = unit;
            _logger = loggerFactory.CreateLogger("Order");
            _paginationOptions = paginationOptions.Value;
        }
        public async Task<bool> AddOrder(Order order, Sales sales)
        {
            try
            {
                //add order
                await _unit.OrderRepository.Add(order);
                await _unit.SaveChangesAsync();
                //add record sales
                await _unit.SalesRepository.Add(sales);
                await _unit.SaveChangesAsync();
                //update inventory
                _updateInventory(order);

                return true;
            }
            catch (Exception e)
            {
                _logger.LogError("Order Add => " + e.Message);
                throw new InternalServerErrorException(e.Message);
            }
        }

        public PagedList<Order> GetOrders(OrderFilter filters)
        {
            filters.PageNumber = filters.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filters.PageNumber;
            filters.PageSize = filters.PageSize == 0 ? _paginationOptions.DefaultPageSize : filters.PageSize;

            try
            {
                var datas = _unit.OrderRepository.GetOrders();

                return PagedList<Order>.Create(datas, filters.PageNumber, filters.PageSize);
            }
            catch (Exception e)
            {
                _logger.LogError("Order List => " + e.Message);
                throw new InternalServerErrorException(e.Message);
            }
        }

        public PagedList<Sales> GetSalesDetail(OrderFilter filters)
        {
            filters.PageNumber = filters.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filters.PageNumber;
            filters.PageSize = filters.PageSize == 0 ? _paginationOptions.DefaultPageSize : filters.PageSize;

            try
            {
                var datas = _unit.SalesRepository.GetSalesDetail();

                return PagedList<Sales>.Create(datas, filters.PageNumber, filters.PageSize);
            }
            catch (Exception e)
            {
                _logger.LogError("Sales List => " + e.Message);
                throw new InternalServerErrorException(e.Message);
            }
        }

        public async Task<bool> UpdateOrderStatus(OrderStatusRequest orderStatus)
        {
            try
            {
                var order = await _unit.OrderRepository.GetById(orderStatus.Id);
                if (order == null)
                {
                    throw new NotFoundException("Customer doesn't exist!");
                }

                order.Status = orderStatus.Status;
                _unit.OrderRepository.Update(order);
                await _unit.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                _logger.LogError("Order status update => " + e.Message);
                throw new InternalServerErrorException(e.Message);
            }
        }

        private async void _updateInventory(Order order)
        {
            foreach(var orderItem in order.OrderItems) 
            { 
                var invetory = await _unit.InventoryRepository.GetByProductId(orderItem.ProductId);
                invetory.Quantity = invetory.Quantity - orderItem.Quantity;
                _unit.InventoryRepository.Update(invetory);
            }
            await _unit.SaveChangesAsync();
        }
    }
}
