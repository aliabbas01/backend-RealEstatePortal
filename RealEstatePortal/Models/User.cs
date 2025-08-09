using System.Text.Json.Serialization;

namespace RealEstatePortal.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; } = string.Empty;

        [JsonIgnore]
        public byte[] PasswordHash { get; set; }

        [JsonIgnore]
        public byte[] PasswordSalt { get; set; }

        [JsonIgnore]
        public List<Favorite> Favorites { get; set; } = new List<Favorite>();

        public string Role { get; set; } = "Buyer"; // Default role 
    }
}