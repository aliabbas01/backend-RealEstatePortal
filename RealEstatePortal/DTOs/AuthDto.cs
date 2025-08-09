using static RealEstatePortal.Common.Constants;
using System.ComponentModel.DataAnnotations;

namespace RealEstatePortal.DTOs
{
    public class UserRegisterDto
    {
        [Required, EmailAddress]
        public string Email { get; set; }

        [Required, StringLength(100, MinimumLength = 6)]
        public string Password { get; set; }

        public string Role { get; set; } = UserRole.Buyer.ToString();
    }

    public class UserLoginDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class UserDto
    {
        public int UserId { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
    }
}