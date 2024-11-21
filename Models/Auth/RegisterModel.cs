using System.ComponentModel.DataAnnotations;

namespace FarmersMarketAPI.Models.Auth
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Username is required")]
        public required string Username { get; set; }

        [Required(ErrorMessage = "First name is required")]
        public required string FirstName { get; set; }

        public string? MiddleName { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        public required string LastName { get; set; }

        [Required(ErrorMessage = "Birth date is required")]
        public DateTime BirthDate { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "Email is required")]
        public required string Email { get; set; }

        [Required(ErrorMessage = "Phone number is required")]
        public required string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public required string Password { get; set; }
    }
}