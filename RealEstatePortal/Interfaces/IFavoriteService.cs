using RealEstatePortal.DTOs;

namespace RealEstatePortal.Interfaces
{
    public interface IFavoriteService
    {
        Task<ServiceResponse<bool>> ToggleFavorite(int propertyId, int userId);
        Task<ServiceResponse<List<PropertyDto>>> GetFavorites(int userId);
    }
}
