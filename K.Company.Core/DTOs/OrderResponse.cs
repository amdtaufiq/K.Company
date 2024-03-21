namespace K.Company.Core.DTOs
{
    public class OrderResponse
    {
        public long Id { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public CustomerResponse Customer { get; set; }
        public virtual ICollection<OrderItemResponse>? OrderItems { get; set; }
    }
}
