namespace FarmersMarketAPI.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public bool Privileged { get; set; }
        public string Fullname { get; set; } = string.Empty;  
        public string Phone { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
    }

}
