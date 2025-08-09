using RealEstatePortal.DTOs;

namespace RealEstatePortal.Interfaces
{
    public interface IPropertyService
    {
        Task<ServiceResponse<List<PropertyDto>>> GetProperties(PropertyFilterDto filter);
        Task<ServiceResponse<PropertyDetailsDto>> GetPropertyDetails(int id);
        Task<ServiceResponse<bool>> ToggleFavorite(int propertyId);
        Task<ServiceResponse<List<PropertyDto>>> GetFavorites();
    }
}
