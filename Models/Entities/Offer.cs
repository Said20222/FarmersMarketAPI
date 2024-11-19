using System.Text.Json.Serialization;
using FarmersMarketAPI.Models.Auth;

namespace FarmersMarketAPI.Models.Entities
{
    public enum OfferStatus {Waiting, Accepted, Rejected}
    public class Offer
    {
        public int OfferId { get; set; }
        public int ProductId { get; set; }
        public Guid CreatedById { get; set; }
        public Guid OfferedToId { get; set; }
        public int OrderId { get; set; }
        public decimal OfferPrice { get; set; }
        public int OfferQuantity { get; set; }
        public OfferStatus OfferStatus{ get; set; }

        [JsonIgnore] public MarketOrder? Order{ get; set; }
        [JsonIgnore] public Product? Product{ get; set; }
        [JsonIgnore] public User? CreatedBy{ get; set; }
        [JsonIgnore] public User? OfferedTo{ get; set; }

    }
}