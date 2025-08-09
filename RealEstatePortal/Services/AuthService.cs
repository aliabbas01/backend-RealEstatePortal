using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RealEstatePortal.Data;
using RealEstatePortal.DTOs;
using RealEstatePortal.Interfaces;
using RealEstatePortal.Models;

namespace RealEstatePortal.Services
{
    public class AuthService : IAuthService
    {
        private readonly DataContext _context;
        private readonly IConfiguration _config;

        public AuthService(DataContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        public async Task<ServiceResponse<string>> Register(UserRegisterDto request)
        {
            var response = new ServiceResponse<string>();
            if (await UserExists(request.Email))
            {
                response.Success = false;
                response.Message = "User already exists.";
                return response;
            }

            CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

            var user = new User
            {
                Email = request.Email,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            response.Data = CreateToken(user);
            response.User = new UserDto()
            {
                UserId = user.Id,
                Email = user.Email,
                Role = user.Role
            };

            return response;
        }

        public async Task<ServiceResponse<string>> Login(UserLoginDto request)
        {
            var response = new ServiceResponse<string>();
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email.ToLower() == request.Email.ToLower());

            if (user == null)
            {
                response.Success = false;
                response.Message = "User not found.";
            }
            else if (!VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
            {
                response.Success = false;
                response.Message = "Wrong password.";
            }
            else
            {
                response.User = new UserDto()
                {
                    UserId = user.Id,
                    Email = user.Email,
                    Role = user.Role
                };
                response.Data = CreateToken(user);
            }

            return response;
        }
        public async Task<User> GetUser(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email.ToLower() == email.ToLower());
        }

        private async Task<bool> UserExists(string email)
        {
            return await _context.Users.AnyAsync(u => u.Email.ToLower() == email.ToLower());
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }

        private string CreateToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email)
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8
                .GetBytes(_config.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}