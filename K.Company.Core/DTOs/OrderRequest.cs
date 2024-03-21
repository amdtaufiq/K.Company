namespace K.Company.Core.DTOs
{
    public class OrderRequest
    {
        public long CustomerID { get; set; }
        public long StoreId { get; set; }

        public virtual ICollection<OrderItemRequest>? OrderItems { get; set; }
    }

    public class OrderItemRequest
    {
        public long ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
