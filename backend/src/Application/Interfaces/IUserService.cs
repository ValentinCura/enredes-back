
using Application.Models;

namespace Application.Interfaces;

public interface IUserService
{
    Task<UserResponseDto> RegisterUserAsync(UserCreateDto userDto);
    Task<UserResponseDto?> GetUserByIdAsync(int id);
    Task<IEnumerable<UserResponseDto>> GetAllUsersAsync();
}