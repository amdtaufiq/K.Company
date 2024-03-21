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
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public ProductController(
            IProductService productService,
            IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAllProduct([FromQuery] ProductFilter filters)
        {
            var products = _productService.GetProductList(filters);
            var metaData = new Metadata()
            {
                TotalCount = products.TotalCount,
                PageSize = products.PageSize,
                CurrentPage = products.CurrentPage,
                TotalPages = products.TotalPages,
                HasNextPage = products.HasNextPage,
                HasPreviousPage = products.HasPreviousPage
            };
            var productDtos = _mapper.Map<IEnumerable<ProductResponse>>(products);

            var response = new ApiResponse<IEnumerable<ProductResponse>>(productDtos)
            {
                Message = new Message
                {
                    Description = "list data product"
                },
                Meta = metaData
            };

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metaData));

            return Ok(response);
        }

        [HttpGet("{productCode}")]
        public async Task<IActionResult> GetProductByID(string productCode)
        {
            var product = await _productService.GeProductById(productCode);
            var productDto = _mapper.Map<ProductResponse>(product);

            var response = new ApiResponse<ProductResponse>(productDto)
            {
                Message = new Message
                {
                    Description = "Detail data product"
                }
            };

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(ProductRequest request)
        {
            var productMap = _mapper.Map<Product>(request);
            var result = await _productService.AddProduct(productMap);

            var response = new ApiResponse<bool>(result)
            {
                Message = new Message
                {
                    Description = "Success create product"
                }
            };

            return Ok(response);
        }

        [HttpPut("{productCode}")]
        public async Task<IActionResult> UpdateProduct(string productCode, ProductRequest request)
        {
            var productMap = _mapper.Map<Product>(request);
            var result = await _productService.UpdateProduct(productCode, productMap);

            var response = new ApiResponse<bool>(result)
            {
                Message = new Message
                {
                    Description = "Success update product"
                }
            };

            return Ok(response);
        }

        [HttpGet("inventory")]
        public IActionResult GetInvetoryDetail([FromQuery] ProductFilter filters)
        {
            var products = _productService.GetInventoryList(filters);
            var metaData = new Metadata()
            {
                TotalCount = products.TotalCount,
                PageSize = products.PageSize,
                CurrentPage = products.CurrentPage,
                TotalPages = products.TotalPages,
                HasNextPage = products.HasNextPage,
                HasPreviousPage = products.HasPreviousPage
            };
            var productDtos = _mapper.Map<IEnumerable<InventoryResponse>>(products);

            var response = new ApiResponse<IEnumerable<InventoryResponse>>(productDtos)
            {
                Message = new Message
                {
                    Description = "list data inventory"
                },
                Meta = metaData
            };

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metaData));

            return Ok(response);
        }
    }
}
