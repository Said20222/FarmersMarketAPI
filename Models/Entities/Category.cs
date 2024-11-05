using System.Text.Json.Serialization;

namespace FarmersMarketAPI.Models.Entities
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string CategoryDescription { get; set; }
        public string CategoryImgPath { get; set; }

        [JsonIgnore] public List<Product> Products{ get; set; }
    }
}