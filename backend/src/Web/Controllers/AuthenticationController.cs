using Application.Interfaces;
using Application.Models;
using Application.Services;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly ICustomAuthenticationService _authService;
        private readonly IUserService _userService;
        public AuthenticationController(ICustomAuthenticationService authService, IUserService userService)
        {
            _authService = authService;
            _userService = userService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] AuthenticationRequestDto request)
        {
            var result = await _authService.Autenticar(request);
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = Request.IsHttps,
                SameSite = Request.IsHttps ? SameSiteMode.None : SameSiteMode.Lax,
                Expires = DateTime.UtcNow.AddHours(1)
            };
            Response.Cookies.Append("token", result.Token, cookieOptions); 
            return Ok(new { message = "Login exitoso", type = result.Type });
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("token");
            return Ok(new { message = "Logout exitoso" });
        }

       
        [Authorize]
        [HttpGet("me")]
        public async Task<IActionResult> GetCurrentUser()
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value
                           ?? User.FindFirst("sub")?.Value;

            if (string.IsNullOrEmpty(userIdClaim))
                return Unauthorized(new { message = "No se encontró el ID en los claims del token" });

            var user = await _userService.GetUserByIdAsync(int.Parse(userIdClaim));
            if (user == null) return NotFound(new { message = "Usuario no encontrado" });

            return Ok(user); // UserResponseDto
        }

    }
}