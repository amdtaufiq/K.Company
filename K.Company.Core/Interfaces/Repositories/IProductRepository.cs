using K.Company.Core.DAOs;

namespace K.Company.Core.Interfaces.Repositories
{
    public interface IProductRepository : IBaseRepository<Product>
    {
        Task<Product> GetByCode(string productCode);
    }
}
