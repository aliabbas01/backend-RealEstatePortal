using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using RealEstatePortal.Data;
using RealEstatePortal.DTOs;
using RealEstatePortal.Interfaces;
using RealEstatePortal.Models;
using System.Security.Claims;

namespace RealEstatePortal.Services
{
    public class PropertyService : IPropertyService
    {
        private readonly DataContext _context;
        private readonly IMapperService _mapper; 
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PropertyService(DataContext context, IMapperService mapper, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _mapper = mapper; 
            _httpContextAccessor = httpContextAccessor;
        }

        private int GetUserId() => int.Parse(_httpContextAccessor.HttpContext.User
            .FindFirstValue(ClaimTypes.NameIdentifier));

        public async Task<ServiceResponse<List<PropertyDto>>> GetProperties(PropertyFilterDto filter)
        {
            var response = new ServiceResponse<List<PropertyDto>>();

            var query = _context.Properties.AsQueryable();

            if (!string.IsNullOrEmpty(filter.City))
                query = query.Where(p => p.City.ToLower().Contains(filter.City.ToLower()));

            if (filter.MinPrice.HasValue)
                query = query.Where(p => p.Price >= filter.MinPrice);

            if (filter.MaxPrice.HasValue)
                query = query.Where(p => p.Price <= filter.MaxPrice);

            if (filter.MinBedrooms.HasValue)
                query = query.Where(p => p.Bedrooms >= filter.MinBedrooms);

            if (!string.IsNullOrEmpty(filter.ListingType))
                query = query.Where(p => p.ListingType == filter.ListingType);

            var properties = await query.ToListAsync();

            // Map using our custom mapper
            var propertyDtos = _mapper.Map<List<PropertyDto>>(properties);

            // If user is logged in, set IsFavorite for each property
            if (_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
            {
                var userId = GetUserId();
                var favoriteIds = await _context.Favorites
                    .Where(f => f.UserId == userId)
                    .Select(f => f.PropertyId)
                    .ToListAsync();

                foreach (var dto in propertyDtos)
                {
                    dto.IsFavorite = favoriteIds.Contains(dto.Id);
                }
            }

            response.Data = propertyDtos;
            return response;
             
        }

        public async Task<ServiceResponse<PropertyDetailsDto>> GetPropertyDetails(int id)
        {
            var response = new ServiceResponse<PropertyDetailsDto>();
            var property = await _context.Properties
                .FirstOrDefaultAsync(p => p.Id == id);

            if (property == null)
            {
                response.Success = false;
                response.Message = "Property not found.";
                return response;
            }

            response.Data = _mapper.Map<PropertyDetailsDto>(property);
            return response;
        }

        public async Task<ServiceResponse<bool>> ToggleFavorite(int propertyId)
        {
            var response = new ServiceResponse<bool>();
            var userId = GetUserId();

            var favorite = await _context.Favorites
                .FirstOrDefaultAsync(f => f.UserId == userId && f.PropertyId == propertyId);

            if (favorite == null)
            {
                _context.Favorites.Add(new Favorite { UserId = userId, PropertyId = propertyId });
                response.Data = true;
                response.Message = "Added to favorites";
            }
            else
            {
                _context.Favorites.Remove(favorite);
                response.Data = false;
                response.Message = "Removed from favorites";
            }

            await _context.SaveChangesAsync();
            return response;
        }

        public async Task<ServiceResponse<List<PropertyDto>>> GetFavorites()
        {
            var response = new ServiceResponse<List<PropertyDto>>();
            var userId = GetUserId();

            var properties = await _context.Favorites
                .Where(f => f.UserId == userId)
                .Select(f => f.Property)
                .ToListAsync();
            var favorites = _mapper.Map<List<PropertyDto>>(properties);

            favorites.ForEach(f => f.IsFavorite = true);
            response.Data = favorites;
            return response;
        }
    }
}