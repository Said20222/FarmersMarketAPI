namespace FarmersMarketAPI.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public int CategoryId { get; set; }
        public int FarmId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public required string ProductDescript { get; set; }

        // Navigation properties
        public required Category Category { get; set; }
        public required Farm Farm { get; set; }
    }
}
