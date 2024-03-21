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
    public class StoreController : ControllerBase
    {
        private readonly IStoreService _storeService;
        private readonly IMapper _mapper;

        public StoreController(
            IStoreService storeService,
            IMapper mapper)
        {
            _storeService = storeService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAllStore([FromQuery] StoreFilter filters)
        {
            var stores = _storeService.GetStoreList(filters);
            var metaData = new Metadata()
            {
                TotalCount = stores.TotalCount,
                PageSize = stores.PageSize,
                CurrentPage = stores.CurrentPage,
                TotalPages = stores.TotalPages,
                HasNextPage = stores.HasNextPage,
                HasPreviousPage = stores.HasPreviousPage
            };
            var storeDtos = _mapper.Map<IEnumerable<StoreResponse>>(stores);

            var response = new ApiResponse<IEnumerable<StoreResponse>>(storeDtos)
            {
                Message = new Message
                {
                    Description = "list data store"
                },
                Meta = metaData
            };

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metaData));

            return Ok(response);
        }

        [HttpGet("{storeCode}")]
        public async Task<IActionResult> GetStoreByID(string storeCode)
        {
            var store = await _storeService.GeStoreById(storeCode);
            var storeDto = _mapper.Map<StoreResponse>(store);

            var response = new ApiResponse<StoreResponse>(storeDto)
            {
                Message = new Message
                {
                    Description = "Detail data store"
                }
            };

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateStore(StoreRequest request)
        {
            var storeMap = _mapper.Map<Store>(request);
            var result = await _storeService.AddStore(storeMap);

            var response = new ApiResponse<bool>(result)
            {
                Message = new Message
                {
                    Description = "Success create store"
                }
            };

            return Ok(response);
        }

        [HttpPut("{storeCode}")]
        public async Task<IActionResult> UpdateProduct(string storeCode, StoreRequest request)
        {
            var storeMap = _mapper.Map<Store>(request);
            var result = await _storeService.UpdateStore(storeCode, storeMap);

            var response = new ApiResponse<bool>(result)
            {
                Message = new Message
                {
                    Description = "Success update store"
                }
            };

            return Ok(response);
        }
    }
}
