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
    public class StoreService : IStoreService
    {
        private readonly IUnitOfWork _unit;
        private readonly ILogger _logger;
        private readonly PaginationOptions _paginationOptions;

        public StoreService(
            IUnitOfWork unit,
            ILoggerFactory loggerFactory,
            IOptions<PaginationOptions> paginationOptions)
        {
            _unit = unit;
            _logger = loggerFactory.CreateLogger("Store");
            _paginationOptions = paginationOptions.Value;
        }

        public async Task<bool> AddStore(Store store)
        {
            var data = await _unit.StoreRepository.GetByCode(store.StoreCode);
            if (data != null)
            {
                throw new UnprocessableEntityException("store code already exists");
            }
            try
            {
                await _unit.StoreRepository.Add(store);
                await _unit.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                _logger.LogError("Store Add => " + e.Message);
                throw new InternalServerErrorException(e.Message);
            }
        }

        public async Task<bool> DeleteStore(string storeCode)
        {
            try
            {
                var data = await _unit.StoreRepository.GetByCode(storeCode);
                if (data == null)
                {
                    throw new NotFoundException("Store doesn't exist!");
                }
                _unit.StoreRepository.Delete(data);
                await _unit.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                _logger.LogError("Store Delete => " + e.Message);
                throw new InternalServerErrorException(e.Message);
            }
        }

        public async Task<Store> GeStoreById(string storeCode)
        {
            try
            {
                var data = await _unit.StoreRepository.GetByCode(storeCode);
                if (data == null)
                {
                    throw new NotFoundException("Store doesn't exist!");
                }
                return data;
            }
            catch (Exception e)
            {
                _logger.LogError("Store By ID => " + e.Message);
                throw new InternalServerErrorException(e.Message);
            }
        }

        public PagedList<Store> GetStoreList(StoreFilter filters)
        {
            filters.PageNumber = filters.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filters.PageNumber;
            filters.PageSize = filters.PageSize == 0 ? _paginationOptions.DefaultPageSize : filters.PageSize;

            try
            {
                var datas = _unit.StoreRepository.GetAll();

                return PagedList<Store>.Create(datas, filters.PageNumber, filters.PageSize);
            }
            catch (Exception e)
            {
                _logger.LogError("Store List => " + e.Message);
                throw new InternalServerErrorException(e.Message);
            }
        }

        public async Task<bool> UpdateStore(string storeCode, Store store)
        {
            try
            {
                var data = await _unit.StoreRepository.GetByCode(storeCode);
                if (data == null)
                {
                    throw new NotFoundException("Store doesn't exist!");
                }
                data.Description = store.Description;
                data.Address = store.Address;
                data.Phone = store.Phone;

                _unit.StoreRepository.Update(data);
                await _unit.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                _logger.LogError("Store Update => " + e.Message);
                throw new InternalServerErrorException(e.Message);
            }
        }
    }
}
