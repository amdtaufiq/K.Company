namespace K.Company.Core.DTOs
{
    public class ProductRequest
    {
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public string ProductType { get; set; }
        public string Brand { get; set; }
        public string UnitOfMeasurement { get; set; }
        public decimal CostOfGoodsSold { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
    }
}
