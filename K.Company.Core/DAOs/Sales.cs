namespace K.Company.Core.DAOs
{
    public class Sales : BaseEntity
    {
        public long StoreId { get; set; }
        public virtual Store Store { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
    }
}
