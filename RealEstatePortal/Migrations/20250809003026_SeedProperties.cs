using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RealEstatePortal.Migrations
{
    /// <inheritdoc />
    public partial class SeedProperties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                INSERT INTO Properties (Title, Address, City, Price, ListingType, Bedrooms, Bathrooms, CarSpots, Description, ImageUrls)
                VALUES 
                ('Modern Downtown Apartment', '123 Main St', 'New York', 750000.00, 'Sale', 2, 2, 1, 
                'Beautiful modern apartment with great views of the city skyline.',
                '[""https://images.unsplash.com/photo-1560448204-e02f11c3d0e2"", 
                  ""https://images.unsplash.com/photo-1493809842364-78817add7ffb""]'),

                ('Suburban Family Home', '456 Oak Ave', 'Chicago', 450000.00, 'Sale', 4, 3, 2, 
                'Spacious family home with large backyard and garage.',
                '[""https://images.unsplash.com/photo-1600585154340-be6161a56a0c""]'),

                ('Luxury Penthouse', '789 High St', 'Miami', 1200000.00, 'Sale', 3, 3, 2, 
                'Stunning penthouse with ocean views and rooftop pool.',
                '[""https://images.unsplash.com/photo-1605276374104-dee2a0ed3cd6"",
                  ""https://images.unsplash.com/photo-1582268611958-ebfd161ef9cf"",
                  ""https://images.unsplash.com/photo-1600566752227-8f3e8a57d521""]'),

                ('Cozy Studio Apartment', '101 Park Lane', 'Boston', 1800.00, 'Rent', 1, 1, 0, 
                'Affordable studio in great downtown location.',
                '[""https://images.unsplash.com/photo-1522708323590-d24dbb6b0267""]'),

                ('Historic Townhouse', '202 Heritage Rd', 'Philadelphia', 650000.00, 'Sale', 3, 2, 1, 
                'Beautifully restored historic townhouse with modern amenities.',
                '[""https://images.unsplash.com/photo-1512917774080-9991f1c4c750"",
                  ""https://images.unsplash.com/photo-1502672260266-1c1ef2d93688""]'),

                ('Beachfront Villa', '303 Coastal Blvd', 'San Diego', 950000.00, 'Sale', 4, 3, 2, 
                'Direct beach access with private patio and stunning sunsets.',
                '[""https://images.unsplash.com/photo-1499793983690-e29da59ef1c2"",
                  ""https://images.unsplash.com/photo-1501785888041-af3ef285b470""]'),

                ('Downtown Loft', '404 Industrial Ave', 'Seattle', 2200.00, 'Rent', 2, 1, 1, 
                'Modern loft with exposed brick and high ceilings.',
                '[""https://images.unsplash.com/photo-1502672023488-70e25813eb80""]'),

                ('Mountain Retreat', '505 Alpine Way', 'Denver', 850000.00, 'Sale', 3, 2, 2, 
                'Peaceful mountain home with panoramic views and hot tub.',
                '[""https://images.unsplash.com/photo-1512917774080-9991f1c4c750"",
                  ""https://images.unsplash.com/photo-1502672260266-1c1ef2d93688""]'),

                ('Urban Condo', '606 Metro Blvd', 'Austin', 550000.00, 'Sale', 2, 2, 1, 
                'Contemporary condo with smart home features and gym access.',
                '[""https://images.unsplash.com/photo-1505691938895-1758d7feb511""]'),

                ('Countryside Cottage', '707 Rural Lane', 'Portland', 350000.00, 'Sale', 2, 1, 1, 
                'Charming cottage with vegetable garden and scenic views.',
                '[""https://images.unsplash.com/photo-1512917774080-9991f1c4c750""]')
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DELETE FROM Properties WHERE Title IN (
                'Modern Downtown Apartment',
                'Suburban Family Home',
                'Luxury Penthouse',
                'Cozy Studio Apartment',
                'Historic Townhouse',
                'Beachfront Villa',
                'Downtown Loft',
                'Mountain Retreat',
                'Urban Condo',
                'Countryside Cottage'
            )");
        }
    }
}
