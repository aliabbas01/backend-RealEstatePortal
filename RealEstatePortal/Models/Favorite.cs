namespace RealEstatePortal.Models
{
    public class Favorite
    {
        public int UserId { get; set; }
        public User User { get; set; }
        public int PropertyId { get; set; }
        public Property Property { get; set; }
        public DateTime DateAdded { get; set; } = DateTime.UtcNow;
    }
}