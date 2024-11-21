using System.Text.Json.Serialization;

namespace FarmersMarketAPI.Models.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public required string CategoryName { get; set; }
        public required string CategoryDescription { get; set; }
        public string? CategoryImgPath { get; set; }

        [JsonIgnore] public List<Product>? Products{ get; set; }
    }
}