namespace K.Company.Core.DTOs
{
    public class SalesResponse
    {
        public long Id { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public virtual StoreResponse Store { get; set; }

    }
}
