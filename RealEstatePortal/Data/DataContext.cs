using Microsoft.EntityFrameworkCore;
using RealEstatePortal.Models;

namespace RealEstatePortal.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Property> Properties { get; set; }
        public DbSet<Favorite> Favorites { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Favorite>()
                .HasKey(f => new { f.UserId, f.PropertyId });
        }
    }
}