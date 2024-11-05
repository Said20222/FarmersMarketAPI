using System.Text.Json.Serialization;

namespace FarmersMarketAPI.Models.Entities
{
    public class Product
    {
        public int ProductId { get; set; }
        public int CategoryId { get; set; }
        public int FarmId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public required string ProductDescript { get; set; }

        [JsonIgnore] public Farm Farm { get; set; }
        [JsonIgnore] public Category Category { get; set; }
        [JsonIgnore] public List<Offer> Offers{ get; set; }

    }
}
