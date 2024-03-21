using AutoMapper;
using K.Company.Core.CustomEntities;
using K.Company.Core.DAOs;
using K.Company.Core.DTOs;
using K.Company.Core.Filters;
using K.Company.Core.Interfaces.Services;
using K.Company.Core.Services.MainServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace K.Company.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        private readonly IMapper _mapper;

        public CustomerController(
            ICustomerService customerService,
            IMapper mapper)
        {
            _customerService = customerService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAllCustomer([FromQuery] CustomerFilter filters)
        {
            var customers = _customerService.GetCustomerList(filters);
            var metaData = new Metadata()
            {
                TotalCount = customers.TotalCount,
                PageSize = customers.PageSize,
                CurrentPage = customers.CurrentPage,
                TotalPages = customers.TotalPages,
                HasNextPage = customers.HasNextPage,
                HasPreviousPage = customers.HasPreviousPage
            };
            var customerDtos = _mapper.Map<IEnumerable<CustomerResponse>>(customers);

            var response = new ApiResponse<IEnumerable<CustomerResponse>>(customerDtos)
            {
                Message = new Message
                {
                    Description = "list data customer"
                },
                Meta = metaData
            };

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metaData));

            return Ok(response);
        }

        [HttpGet("{customerId}")]
        public async Task<IActionResult> GetCustomerByID(long customerId)
        {
            var customer = await _customerService.GeCustomerById(customerId);
            var customerDto = _mapper.Map<CustomerResponse>(customer);

            var response = new ApiResponse<CustomerResponse>(customerDto)
            {
                Message = new Message
                {
                    Description = "Detail data customer"
                }
            };

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCustomer(CustomerRequest request)
        {
            var customerMap = _mapper.Map<Customer>(request);
            var result = await _customerService.AddCustomer(customerMap);

            var response = new ApiResponse<bool>(result)
            {
                Message = new Message
                {
                    Description = "Success create customer"
                }
            };

            return Ok(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(long id, CustomerRequest request)
        {
            var customerMap = _mapper.Map<Customer>(request);
            var result = await _customerService.UpdateCustomer(id, customerMap);

            var response = new ApiResponse<bool>(result)
            {
                Message = new Message
                {
                    Description = "Success update customer"
                }
            };

            return Ok(response);
        }
    }
}
