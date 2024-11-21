using System.ComponentModel.DataAnnotations;
using FarmersMarketAPI.Models.Entities;

namespace FarmersMarketAPI.Models.Auth
{
    public class FarmerRegisterModel : RegisterModel {
        public string? ProfileImgPath { get; set; }

        [Required(ErrorMessage = "Farm name is required")]
        public required string FarmName { get; set;}

        [Required(ErrorMessage = "Farm location is required")]
        public required string Location { get; set; }

        [Required(ErrorMessage = "Farm size is required")]
        public FarmSize FarmSize{ get; set; }
        public string? FarmDescription { get; set; }
    }
}