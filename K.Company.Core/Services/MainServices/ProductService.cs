using K.Company.Core.CustomEntities;
using K.Company.Core.DAOs;
using K.Company.Core.Exceptions;
using K.Company.Core.Filters;
using K.Company.Core.Interfaces.Services;
using K.Company.Core.Interfaces.Unit;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace K.Company.Core.Services.MainServices
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unit;
        private readonly ILogger _logger;
        private readonly PaginationOptions _paginationOptions;

        public ProductService(
            IUnitOfWork unit,
            ILoggerFactory loggerFactory,
            IOptions<PaginationOptions> paginationOptions)
        {
            _unit = unit;
            _logger = loggerFactory.CreateLogger("Product");
            _paginationOptions = paginationOptions.Value;
        }

        public async Task<bool> AddProduct(Product product)
        {
            var data = await _unit.ProductRepository.GetByCode(product.ProductCode);
            if (data != null)
            {
                throw new UnprocessableEntityException("product code already exists");
            }
            try
            {
                await _unit.ProductRepository.Add(product);
                await _unit.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                _logger.LogError("Product Add => " + e.Message);
                throw new InternalServerErrorException(e.Message);
            }
        }

        public async Task<bool> DeleteProduct(string productCode)
        {
            try
            {
                var data = await _unit.ProductRepository.GetByCode(productCode);
                if (data == null)
                {
                    throw new NotFoundException("Product doesn't exist!");
                }
                _unit.ProductRepository.Delete(data);
                await _unit.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                _logger.LogError("Product Delete => " + e.Message);
                throw new InternalServerErrorException(e.Message);
            }
        }

        public async Task<Product> GeProductById(string productCode)
        {
            try
            {
                var data = await _unit.ProductRepository.GetByCode(productCode);
                if (data == null)
                {
                    throw new NotFoundException("Product doesn't exist!");
                }
                return data;
            }
            catch (Exception e)
            {
                _logger.LogError("Product By ID => " + e.Message);
                throw new InternalServerErrorException(e.Message);
            }
        }


        public PagedList<Product> GetProductList(ProductFilter filters)
        {
            filters.PageNumber = filters.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filters.PageNumber;
            filters.PageSize = filters.PageSize == 0 ? _paginationOptions.DefaultPageSize : filters.PageSize;

            try
            {
                var datas = _unit.ProductRepository.GetAll();

                return PagedList<Product>.Create(datas, filters.PageNumber, filters.PageSize);
            }
            catch (Exception e)
            {
                _logger.LogError("Product List => " + e.Message);
                throw new InternalServerErrorException(e.Message);
            }
        }

        public async Task<bool> UpdateProduct(string productCode, Product product)
        {
            try
            {
                var data = await _unit.ProductRepository.GetByCode(productCode);
                if (data == null)
                {
                    throw new NotFoundException("Product doesn't exist!");
                }
                data.ProductName = product.ProductName;
                data.Description = product.Description;
                data.ProductType = product.ProductType;
                data.Brand = product.Brand;
                data.UnitOfMeasurement = product.UnitOfMeasurement;
                data.CostOfGoodsSold = product.CostOfGoodsSold;
                data.UnitPrice = product.UnitPrice;

                _unit.ProductRepository.Update(data);
                await _unit.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                _logger.LogError("Product Update => " + e.Message);
                throw new InternalServerErrorException(e.Message);
            }
        }

        public PagedList<Inventory> GetInventoryList(ProductFilter filters)
        {
            filters.PageNumber = filters.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filters.PageNumber;
            filters.PageSize = filters.PageSize == 0 ? _paginationOptions.DefaultPageSize : filters.PageSize;

            try
            {
                var datas = _unit.InventoryRepository.GetDetailInvetory();

                return PagedList<Inventory>.Create(datas, filters.PageNumber, filters.PageSize);
            }
            catch (Exception e)
            {
                _logger.LogError("Inventory List => " + e.Message);
                throw new InternalServerErrorException(e.Message);
            }
        }
    }
}
