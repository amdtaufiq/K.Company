namespace K.Company.Core.DAOs
{
    public class Customer : BaseEntity
    {
        public string CustomerName { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string CustomerType { get; set; }
    }
}
