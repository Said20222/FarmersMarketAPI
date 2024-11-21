using System.Text.Json.Serialization;
using FarmersMarketAPI.Models.Auth;

namespace FarmersMarketAPI.Models.Entities
{
    public enum OrderStatus {Processing, Dispatched, Delivered}
    public class MarketOrder
    {
        public int Id { get; set;}
        public Guid BuyerId { get; set;}
        public DeliveryMethod DeliveryMethod{ get; set;}
        public PaymentMethod PaymentMethod{ get; set;}
        public DateOnly DeliverytDate { get; set;}
        public OrderStatus OrderStatus { get; set;}

        [JsonIgnore] public User? Buyer  { get; set;}
        [JsonIgnore] public List<Offer>? Offers { get; set;}
    }
}