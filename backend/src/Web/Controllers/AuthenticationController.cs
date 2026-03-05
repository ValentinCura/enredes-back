using Application.Interfaces;
using Application.Models;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult Login([FromBody] AuthenticationRequestDto request)
        {
            var token = _authService.Autenticar(request);

            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,      // JavaScript no puede leerla
                Secure = true,        // Solo HTTPS
                SameSite = SameSiteMode.None, // Necesario para cross-origin
                Expires = DateTime.UtcNow.AddHours(1)
            };

            Response.Cookies.Append("token", token, cookieOptions);

            return Ok(new { message = "Login exitoso" });
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
            var userId = User.FindFirst("sub")?.Value;
            if (userId == null) return Unauthorized();

            var user = await _userService.GetUserByIdAsync(int.Parse(userId));
            if (user == null) return NotFound();

            return Ok(user);
        }
    }
}