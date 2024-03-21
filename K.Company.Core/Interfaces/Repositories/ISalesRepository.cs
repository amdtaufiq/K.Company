using K.Company.Core.DAOs;

namespace K.Company.Core.Interfaces.Repositories
{
    public interface ISalesRepository : IBaseRepository<Sales>
    {
        IEnumerable<Sales> GetSalesDetail();
    }
}
