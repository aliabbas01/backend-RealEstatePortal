namespace RealEstatePortal.DTOs
{
    public class PropertyDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public decimal Price { get; set; }
        public string ListingType { get; set; }
        public int Bedrooms { get; set; }
        public int Bathrooms { get; set; }
        public List<string> ImageUrls { get; set; }
        public bool IsFavorite { get; set; }
    }

    public class PropertyDetailsDto : PropertyDto
    {
        public string Description { get; set; }
        public int CarSpots { get; set; }
    }

    public class PropertyFilterDto
    {
        public string? City { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public int? MinBedrooms { get; set; }
        public string? ListingType { get; set; }
    }
}