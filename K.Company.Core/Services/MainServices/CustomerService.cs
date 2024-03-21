using K.Company.Core.CustomEntities;
using K.Company.Core.DAOs;
using K.Company.Core.Exceptions;
using K.Company.Core.Filters;
using K.Company.Core.Interfaces.Services;
using K.Company.Core.Interfaces.Unit;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

public class CustomerService : ICustomerService
{
    private readonly IUnitOfWork _unit;
    private readonly ILogger _logger;
    private readonly PaginationOptions _paginationOptions;

    public CustomerService(
        IUnitOfWork unit,
        ILoggerFactory loggerFactory,
        IOptions<PaginationOptions> paginationOptions)
    {
        _unit = unit;
        _logger = loggerFactory.CreateLogger("Customer");
        _paginationOptions = paginationOptions.Value;
    }

    public async Task<bool> AddCustomer(Customer customer)
    {
        try
        {
            await _unit.CustomerRepository.Add(customer);
            await _unit.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            _logger.LogError("Customer Add => " + e.Message);
            throw new InternalServerErrorException(e.Message);
        }
    }

    public async Task<bool> DeleteCustomer(long customerId)
    {
        try
        {
            var data = await _unit.CustomerRepository.GetById(customerId);
            if (data == null)
            {
                throw new NotFoundException("Customer doesn't exist!");
            }
            _unit.CustomerRepository.Delete(data);
            await _unit.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            _logger.LogError("Customer Delete => " + e.Message);
            throw new InternalServerErrorException(e.Message);
        }
    }

    public async Task<Customer> GeCustomerById(long customerId)
    {
        try
        {
            var data = await _unit.CustomerRepository.GetById(customerId);
            if (data == null)
            {
                throw new NotFoundException("Customer doesn't exist!");
            }
            return data;
        }
        catch (Exception e)
        {
            _logger.LogError("Customer By ID => " + e.Message);
            throw new InternalServerErrorException(e.Message);
        }
    }

    public PagedList<Customer> GetCustomerList(CustomerFilter filters)
    {
        filters.PageNumber = filters.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filters.PageNumber;
        filters.PageSize = filters.PageSize == 0 ? _paginationOptions.DefaultPageSize : filters.PageSize;

        try
        {
            var datas = _unit.CustomerRepository.GetAll();

            return PagedList<Customer>.Create(datas, filters.PageNumber, filters.PageSize);
        }
        catch (Exception e)
        {
            _logger.LogError("Customer List => " + e.Message);
            throw new InternalServerErrorException(e.Message);
        }
    }

    public async Task<bool> UpdateCustomer(long customerId, Customer customer)
    {
        try
        {
            var data = await _unit.CustomerRepository.GetById(customerId);
            if (data == null)
            {
                throw new NotFoundException("Customer doesn't exist!");
            }
            data.CustomerName = customer.CustomerName;
            data.Address = customer.Address;
            data.Phone = customer.Phone;
            data.CustomerType = customer.CustomerType;

            _unit.CustomerRepository.Update(data);
            await _unit.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            _logger.LogError("Customer Update => " + e.Message);
            throw new InternalServerErrorException(e.Message);
        }
    }
}