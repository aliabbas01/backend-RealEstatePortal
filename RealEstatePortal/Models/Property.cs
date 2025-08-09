using System.Text.Json.Serialization;

namespace RealEstatePortal.Models
{
    public class Property
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string ListingType { get; set; } = "Sale"; // "Rent" or "Sale"
        public int Bedrooms { get; set; }
        public int Bathrooms { get; set; }
        public int CarSpots { get; set; }
        public string Description { get; set; } = string.Empty;
        public string ImageUrls { get; set; } = "[]"; // JSON array of URLs

        [JsonIgnore]
        public List<Favorite> Favorites { get; set; } = new List<Favorite>();
    }
}