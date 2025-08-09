using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealEstatePortal.DTOs;
using RealEstatePortal.Interfaces;
using RealEstatePortal.Services;

namespace RealEstatePortal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropertyController : ControllerBase
    {
        private readonly IPropertyService _propertyService;
        private readonly IFavoriteService _favoriteService;
        public PropertyController(IPropertyService propertyService, IFavoriteService favoriteService)
        {
            _propertyService = propertyService;
            _favoriteService = favoriteService;
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<PropertyDto>>>> GetProperties([FromQuery] PropertyFilterDto filter)
        {
            return Ok(await _propertyService.GetProperties(filter));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<PropertyDetailsDto>>> GetProperty(int id)
        {
            return Ok(await _propertyService.GetPropertyDetails(id));
        }

        [HttpPost("favorites/{propertyId}")]
        [Authorize]
        public async Task<ActionResult<ServiceResponse<bool>>> ToggleFavorite(int propertyId)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            return Ok(await _favoriteService.ToggleFavorite(propertyId, userId));
        }

        [HttpGet("favorites")]
        [Authorize]
        public async Task<ActionResult<ServiceResponse<List<PropertyDto>>>> GetFavorites()
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            return Ok(await _favoriteService.GetFavorites(userId));
        }
    }
}