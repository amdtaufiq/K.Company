using System.ComponentModel.DataAnnotations.Schema;

namespace K.Company.Core.DAOs
{
    public class Store : BaseEntity
    {
        public string StoreCode { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
    }
}
