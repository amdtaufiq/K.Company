using K.Company.Core.DAOs;
using K.Company.Core.Interfaces.Repositories;
using K.Company.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace K.Company.Infrastructure.Repositories
{
    public class SalesRepository : BaseRepository<Sales>, ISalesRepository
    {
        public SalesRepository(KCompDBContext context) : base(context)
        {

        }

        public IEnumerable<Sales> GetSalesDetail()
        {
            return _ctx.Sales
                .Include(x => x.Store)
                .AsEnumerable();
        }
    }
}
