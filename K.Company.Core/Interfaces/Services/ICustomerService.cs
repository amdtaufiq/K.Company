using K.Company.Core.CustomEntities;
using K.Company.Core.DAOs;
using K.Company.Core.Filters;

namespace K.Company.Core.Interfaces.Services
{
    public interface ICustomerService
    {
        PagedList<Customer> GetCustomerList(CustomerFilter filters);
        Task<Customer> GeCustomerById(long customerId);
        Task<bool> AddCustomer(Customer customer);
        Task<bool> UpdateCustomer(long customerId, Customer customer);
        Task<bool> DeleteCustomer(long customerId);
    }
}
