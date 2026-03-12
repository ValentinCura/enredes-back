
using Application.Interfaces; // Aquí estará la interfaz del servicio
using Application.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
    [Authorize(Roles = "Admin")]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var user = await _userService.GetUserByIdAsync(id);
        if (user == null) return NotFound();
        return Ok(user);
    }
    [Authorize(Roles = "Admin")]
    [HttpGet("actives")]
    public async Task<IActionResult> GetActives()
    {
        var users = await _userService.GetActiveUsersAsync();
        return Ok(users);
    }
    [Authorize(Roles = "Admin")]
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

    [Authorize(Roles = "Admin")]
    [HttpPost("create-admin")]
    public async Task<IActionResult> CreateAdmin([FromBody] UserCreateDto userDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var result = await _userService.CreateAdminAsync(userDto);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
    [Authorize]
    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UserUpdateDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                          ?? User.FindFirst("sub")?.Value;

        if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId))
            return Unauthorized(new { message = "No se pudo identificar al usuario" });

        var user = await _userService.UpdateUserAsync(userId, dto);

        if (user == null)
            return NotFound(new { message = "Usuario no encontrado" });

        return Ok(user);
    }
    [Authorize(Roles = "Admin")]
    [HttpPatch("{id}/status")]
    public async Task<IActionResult> ChangeStatus(int id, [FromBody] ChangeStatusDto dto)
    {
        var result = await _userService.ChangeStatusAsync(id, dto.Status);
        if (!result) return NotFound();
        return Ok(new { message = "Estado actualizado correctamente" });
    }
    [Authorize(Roles = "Admin")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _userService.DeleteUserAsync(id);
        if (!result) return NotFound();
        return NoContent();
    }

}