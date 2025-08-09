using RealEstatePortal.DTOs;
using RealEstatePortal.Models;

namespace RealEstatePortal.Interfaces
{
    public interface ITokenService
    {
        public string CreateToken(User user);
    }
}
