using RealEstatePortal.Models;

namespace RealEstatePortal.DTOs
{
    public class ServiceResponse<T>
    {
        public T? Data { get; set; }
        public bool Success { get; set; } = true;
        public string Message { get; set; } = string.Empty;
        public UserDto User { get; set; }
    }
}