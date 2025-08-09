using AutoMapper; 
using Microsoft.EntityFrameworkCore;
using RealEstatePortal.Data;
using RealEstatePortal.DTOs;
using RealEstatePortal.Interfaces;
using RealEstatePortal.Models; 

namespace RealEstatePortal.Services
{
    public class FavoriteService : IFavoriteService
    {
        private readonly DataContext _context;
        private readonly IMapperService _mapper;

        public FavoriteService(DataContext context, IMapperService mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<bool>> ToggleFavorite(int propertyId, int userId)
        {
            var favorite = await _context.Favorites
                .FirstOrDefaultAsync(f => f.UserId == userId && f.PropertyId == propertyId);

            if (favorite == null)
            {
                _context.Favorites.Add(new Favorite { UserId = userId, PropertyId = propertyId });
                await _context.SaveChangesAsync();
                return new ServiceResponse<bool> { Data = true, Message = "Added to favorites" };
            }

            _context.Favorites.Remove(favorite);
            await _context.SaveChangesAsync();
            return new ServiceResponse<bool> { Data = false, Message = "Removed from favorites" };
        }

        public async Task<ServiceResponse<List<PropertyDto>>> GetFavorites(int userId)
        {
            var favorites = await _context.Favorites
                .Where(f => f.UserId == userId)
                .Include(f => f.Property)
                .Select(f => _mapper.Map<PropertyDto>(f.Property))
                .ToListAsync();

            favorites.ForEach(p => p.IsFavorite = true);
            return new ServiceResponse<List<PropertyDto>> { Data = favorites };
        }
    }
}