using Microsoft.AspNetCore.Identity;
using FarmersMarketAPI.Models.Entities;
using System.Text.Json.Serialization;

namespace FarmersMarketAPI.Models.Auth
{

    public class User : IdentityUser<Guid>
    {
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? ProfileImgPath { get; set; }
        public PaymentMethod? PreferredPaymentMethod { get; set; }
        public DeliveryMethod? PreferredDeliveryMethod { get; set; }
        [JsonIgnore] public List<Farm> Farms { get; set; }
        [JsonIgnore] public List<MarketOrder> Orders { get; set; }
        [JsonIgnore] public List<Offer> CreatedOffers { get; set; }
        [JsonIgnore] public List<Offer> ReceivedOffers { get; set; }
    }
}