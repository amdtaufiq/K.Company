namespace K.Company.Core.DAOs
{
    public class Order : BaseEntity
    {
        public long CustomerID { get; set; }
        public virtual Customer Customer { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; }
        public virtual ICollection<OrderItem>? OrderItems { get; set; }
    }
}
