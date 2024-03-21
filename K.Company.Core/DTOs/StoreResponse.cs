using System.ComponentModel.DataAnnotations.Schema;

namespace K.Company.Core.DTOs
{
    public class StoreResponse
    {
        public long Id { get; set; }
        public string StoreCode { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
