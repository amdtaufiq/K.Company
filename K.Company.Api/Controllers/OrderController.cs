using AutoMapper;
using K.Company.Core.CustomEntities;
using K.Company.Core.DAOs;
using K.Company.Core.DTOs;
using K.Company.Core.Filters;
using K.Company.Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace K.Company.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;

        public OrderController(
            IOrderService orderService,
            IMapper mapper)
        {
            _orderService = orderService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder(OrderRequest request)
        {
            var orderMap = _mapper.Map<Order>(request);
            var sales = new Sales
            {
                StoreId = request.StoreId,
                Date = DateTime.UtcNow,
                Amount = request.OrderItems.Sum(x => x.Quantity * x.UnitPrice)
            };
            var result = await _orderService.AddOrder(orderMap, sales);

            var response = new ApiResponse<bool>(result)
            {
                Message = new Message
                {
                    Description = "Success create order"
                }
            };

            return Ok(response);
        }

        [HttpPut("status")]
        public async Task<IActionResult> UpdateOrderStatus(OrderStatusRequest request)
        {
            var result = await _orderService.UpdateOrderStatus(request);

            var response = new ApiResponse<bool>(result)
            {
                Message = new Message
                {
                    Description = "Success update order status"
                }
            };

            return Ok(response);
        }

        [HttpGet("sales")]
        public IActionResult GetSalesDetail([FromQuery] OrderFilter filters)
        {
            var sales = _orderService.GetSalesDetail(filters);
            var metaData = new Metadata()
            {
                TotalCount = sales.TotalCount,
                PageSize = sales.PageSize,
                CurrentPage = sales.CurrentPage,
                TotalPages = sales.TotalPages,
                HasNextPage = sales.HasNextPage,
                HasPreviousPage = sales.HasPreviousPage
            };
            var salesDtos = _mapper.Map<IEnumerable<SalesResponse>>(sales);

            var response = new ApiResponse<IEnumerable<SalesResponse>>(salesDtos)
            {
                Message = new Message
                {
                    Description = "list data sales"
                },
                Meta = metaData
            };

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metaData));

            return Ok(response);
        }

        [HttpGet]
        public IActionResult GetOrders([FromQuery] OrderFilter filters)
        {
            var order = _orderService.GetOrders(filters);
            var metaData = new Metadata()
            {
                TotalCount = order.TotalCount,
                PageSize = order.PageSize,
                CurrentPage = order.CurrentPage,
                TotalPages = order.TotalPages,
                HasNextPage = order.HasNextPage,
                HasPreviousPage = order.HasPreviousPage
            };
            var orderDtos = _mapper.Map<IEnumerable<OrderResponse>>(order);

            var response = new ApiResponse<IEnumerable<OrderResponse>>(orderDtos)
            {
                Message = new Message
                {
                    Description = "list data order"
                },
                Meta = metaData
            };

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metaData));

            return Ok(response);
        }
    }
}
