namespace K.Company.Core.DTOs
{
    public class InventoryResponse
    {
        public long Id { get; set; }
        public int Quantity { get; set; }
        public virtual ProductResponse Product { get; set; }
    }
}
