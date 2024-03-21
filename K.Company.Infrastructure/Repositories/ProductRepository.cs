using K.Company.Core.DAOs;
using K.Company.Core.Interfaces.Repositories;
using K.Company.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace K.Company.Infrastructure.Repositories
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        public ProductRepository(KCompDBContext context) : base(context)
        {
            
        }

        public async Task<Product> GetByCode(string productCode)
        {
            return await _ctx.Products.Where(x => x.ProductCode == productCode).FirstOrDefaultAsync();
        }
    }
}
