using System.Text.Json.Serialization;
using FarmersMarketAPI.Models.Auth;

namespace FarmersMarketAPI.Models.Entities
{
    public enum FarmSize {Small, Medium, Large}
    public class Farm
    {
        public int FarmId { get; set; }
        public string FarmName { get; set;}
        public Guid FarmerId { get; set; }
        public string Location { get; set; }
        public FarmSize FarmSize{ get; set; }
        [JsonIgnore] public User? Farmer { get; set; }
        [JsonIgnore] public List<Product>? Products{ get; set; }

    }
}