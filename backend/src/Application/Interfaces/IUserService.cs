
using Application.Models;

namespace Application.Interfaces;

public interface IUserService
{
    Task<UserResponseDto> RegisterUserAsync(UserCreateDto userDto);
    Task<UserResponseDto?> GetUserByIdAsync(int id);
    Task<IEnumerable<UserResponseDto>> GetAllUsersAsync();
    Task<UserResponseDto?> UpdateUserAsync(int id, UserUpdateDto dto);
    Task<bool> DeleteUserAsync(int id);
    Task<IEnumerable<UserResponseDto>> GetActiveUsersAsync();
    Task<bool> ChangeStatusAsync(int id, bool status);

}