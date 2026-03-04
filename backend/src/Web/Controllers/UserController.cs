
using Application.Interfaces; // Aquí estará la interfaz del servicio
using Application.Models;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }


    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var user = await _userService.GetUserByIdAsync(id);
        if (user == null) return NotFound();
        return Ok(user);
    }
    [HttpGet("actives")]
    public async Task<IActionResult> GetActives()
    {
        var users = await _userService.GetActiveUsersAsync();
        return Ok(users);
    }
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        // El servicio nos devuelve una lista de UserResponseDto (sin contraseñas)
        var users = await _userService.GetAllUsersAsync();

        // Retornamos un 200 OK con la lista
        return Ok(users);
    }
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] UserCreateDto userDto)
    {
        var result = await _userService.RegisterUserAsync(userDto);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UserUpdateDto dto)
    {
        var user = await _userService.UpdateUserAsync(id, dto);
        if (user == null) return NotFound();
        return Ok(user);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _userService.DeleteUserAsync(id);
        if (!result) return NotFound();
        return NoContent();
    }

}