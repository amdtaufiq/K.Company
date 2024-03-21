namespace K.Company.Core.DAOs
{
    public class Inventory : BaseEntity
    {
        public long ProductId { get; set; }
        public virtual Product Product { get; set; }
        public int Quantity { get; set; }
    }
}
