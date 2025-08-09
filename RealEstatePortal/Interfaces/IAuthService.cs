using RealEstatePortal.DTOs;
using RealEstatePortal.Models;

namespace RealEstatePortal.Interfaces
{
    public interface IAuthService
    {
        Task<ServiceResponse<string>> Register(UserRegisterDto request);
        Task<ServiceResponse<string>> Login(UserLoginDto request);
        Task<User> GetUser(string email);
    }
}
