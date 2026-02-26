
using Application.Interfaces; // Aquí estará la interfaz del servicio
using Application.Models;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] UserCreateDto userDto)
    {
        var result = await _userService.RegisterUserAsync(userDto);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var user = await _userService.GetUserByIdAsync(id);
        if (user == null) return NotFound();
        return Ok(user);
    }
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        // El servicio nos devuelve una lista de UserResponseDto (sin contraseñas)
        var users = await _userService.GetAllUsersAsync();

        // Retornamos un 200 OK con la lista
        return Ok(users);
    }
}