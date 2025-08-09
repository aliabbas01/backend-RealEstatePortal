using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RealEstatePortal.DTOs;
using RealEstatePortal.Interfaces;
using RealEstatePortal.Models;
using RealEstatePortal.Services;

namespace RealEstatePortal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly ITokenService _tokenService;
        public AuthController(IAuthService authService, ITokenService tokenService)
        {
            _authService = authService;
            _tokenService = tokenService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<ServiceResponse<string>>> Register(UserRegisterDto request)
        {
            var response = await _authService.Register(request);

            if (!response.Success)
                return BadRequest(response);

            return Ok(response);
        }

        [HttpPost("login")]
        public async Task<ActionResult<ServiceResponse<string>>> Login(UserLoginDto request)
        {
            var response = await _authService.Login(request);

            if (!response.Success)
                return BadRequest(response);

            var user = await _authService.GetUser(request.Email);
            response.Data = _tokenService.CreateToken(user);
            return Ok(response);
        }
    }
}