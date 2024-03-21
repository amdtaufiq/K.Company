using System.ComponentModel.DataAnnotations.Schema;

namespace K.Company.Core.DAOs
{
    public class OrderItem : BaseEntity
    {
        public long OrderID { get; set; }
        public virtual Order Order { get; set; }
        public long ProductId { get; set; }
        public virtual Product Product { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
