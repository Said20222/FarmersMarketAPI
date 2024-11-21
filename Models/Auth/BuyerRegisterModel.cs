using System.ComponentModel.DataAnnotations;

namespace FarmersMarketAPI.Models.Auth
{
    public class BuyerRegisterModel : RegisterModel {
        public PaymentMethod? PreferredPaymentMethod { get; set; }

        public DeliveryMethod? PreferredDeliveryMethod { get; set; }

        public string? DeliveryAddress { get; set; }
    }
}