using K.Company.Core.CustomEntities;
using K.Company.Core.DAOs;
using K.Company.Core.Filters;

namespace K.Company.Core.Interfaces.Services
{
    public interface IProductService
    {
        PagedList<Product> GetProductList(ProductFilter filters);
        Task<Product> GeProductById(string productCode);
        Task<bool> AddProduct(Product product);
        Task<bool> UpdateProduct(string productCode, Product product);
        Task<bool> DeleteProduct(string productCode);
        PagedList<Inventory> GetInventoryList(ProductFilter filters);
    }
}
