namespace K.Company.Core.DTOs
{
    public class OrderItemResponse
    {
        public long Id { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
        public virtual ProductResponse Product { get; set; }
    }
}
