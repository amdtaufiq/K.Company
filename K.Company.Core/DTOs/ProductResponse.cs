namespace K.Company.Core.DTOs
{
    public class ProductResponse
    {
        public long Id { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public string ProductType { get; set; }
        public string Brand { get; set; }
        public string UnitOfMeasurement { get; set; }
        public decimal CostOfGoodsSold { get; set; }
        public decimal UnitPrice { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
