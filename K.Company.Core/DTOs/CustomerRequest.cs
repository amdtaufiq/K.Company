namespace K.Company.Core.DTOs
{
    public class CustomerRequest
    {
        public long Id { get; set; }
        public string CustomerName { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string CustomerType { get; set; }
    }
}
