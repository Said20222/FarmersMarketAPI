using System.Text.Json.Serialization;
using FarmersMarketAPI.Models.Auth;

namespace FarmersMarketAPI.Models.Entities
{
    public enum FarmSize {Small, Medium, Large}
    public class Farm
    {
        public int Id { get; set; }
        public required string FarmName { get; set;}
        public Guid FarmerId { get; set; }
        public required string Location { get; set; }
        public FarmSize FarmSize{ get; set; }
        public string? FarmDescription { get; set; }
        [JsonIgnore] public User? Farmer { get; set; }
        [JsonIgnore] public List<Product>? Products{ get; set; }

    }
}