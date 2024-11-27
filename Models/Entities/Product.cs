using System.Text.Json.Serialization;

namespace FarmersMarketAPI.Models.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public required string ProductName { get; set; }
        public int CategoryId { get; set; }
        public int FarmId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string? ProductDescript { get; set; }

        [JsonIgnore] public Farm? Farm { get; set; }
        [JsonIgnore] public Category? Category { get; set; }
        [JsonIgnore] public List<Offer>? Offers{ get; set; }
        [JsonIgnore] public List<ProductImage>? ProductImages { get; set; }

    }

    public class ProductImage {
        public int ProductImageId { get; set; }
        public int ProductId { get; set; }
        public required string ImagePath { get; set; }[JsonIgnore]
        public required virtual Product Product { get; set; }
    }
}
