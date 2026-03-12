using Application.Models;
using Application.Interfaces;
using Domain.Entities;
using BCrypt.Net;
namespace Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<UserResponseDto> RegisterUserAsync(UserCreateDto userDto)
    {
        var existingUser = await _userRepository.GetByEmailAsync(userDto.Email);
        if (existingUser != null)
            throw new ArgumentException("El email ya está registrado");
        var user = new User
        {
           
            Email = userDto.Email,
            Password = BCrypt.Net.BCrypt.HashPassword(userDto.Password),
            Firstname = userDto.FirstName,
            Lastname = userDto.LastName,
            ClientNumber = userDto.ClientNumber,
            Phonenumber = userDto.Phonenumber,
            Type = "Client"
        };

        await _userRepository.AddAsync(user);
        // Al hacer SaveChanges (dentro del add), EF Core actualizará el objeto 'user' con el ID numérico generado.

        return new UserResponseDto
        {
            Id = user.Id, // Aquí 'Id' ya tendrá el valor 1, 2, 3...
            Email = user.Email,
            FullName = $"{user.Firstname} {user.Lastname}",
            ClientNumber = user.ClientNumber,
            Type = user.Type,
            Phonenumber = userDto.Phonenumber
        };
    }

    public async Task<UserResponseDto?> GetUserByIdAsync(int id)
    {
        var user = await _userRepository.GetByIdAsync(id);
        if (user == null) return null;

        return new UserResponseDto
        {
            Id = user.Id,
            Email = user.Email,
            FullName = $"{user.Firstname} {user.Lastname}",
            ClientNumber = user.ClientNumber,
            Phonenumber = user.Phonenumber
        };
    }
    public async Task<IEnumerable<UserResponseDto>> GetAllUsersAsync()
    {
        // 1. Obtenemos todos los usuarios desde el repositorio (usando el BaseRepository)
        var users = await _userRepository.GetAllAsync();

        // 2. Mapeamos la lista de Entidades a una lista de DTOs
        return users.Select(user => new UserResponseDto
        {
            Id = user.Id,
            Email = user.Email,
            FullName = $"{user.Firstname} {user.Lastname}",
            ClientNumber = user.ClientNumber,
            Type = user.Type, // O el valor que tengas en la entidad,
            Phonenumber = user.Phonenumber,
            Status = user.Status
        });
    }
    public async Task<UserResponseDto?> UpdateUserAsync(int id, UserUpdateDto dto)
    {
        // Validar que al menos un campo tenga valor
        if (string.IsNullOrWhiteSpace(dto.Email) &&
            string.IsNullOrWhiteSpace(dto.Password) &&
            string.IsNullOrWhiteSpace(dto.FirstName) &&
            string.IsNullOrWhiteSpace(dto.LastName) &&
            string.IsNullOrWhiteSpace(dto.ClientNumber) &&
            string.IsNullOrWhiteSpace(dto.Phonenumber))
        {
            throw new InvalidOperationException("Debe proporcionar al menos un campo para actualizar");
        }

        var user = await _userRepository.GetByIdAsync(id);
        if (user == null) return null;

        if (!string.IsNullOrWhiteSpace(dto.Email))
            user.Email = dto.Email;

        if (!string.IsNullOrWhiteSpace(dto.Password))
            user.Password = BCrypt.Net.BCrypt.HashPassword(dto.Password);

        if (!string.IsNullOrWhiteSpace(dto.FirstName))
            user.Firstname = dto.FirstName;

        if (!string.IsNullOrWhiteSpace(dto.LastName))
            user.Lastname = dto.LastName;

        if (!string.IsNullOrWhiteSpace(dto.ClientNumber))
            user.ClientNumber = dto.ClientNumber;

        if (!string.IsNullOrWhiteSpace(dto.Phonenumber))
            user.Phonenumber = dto.Phonenumber;

        await _userRepository.UpdateAsync(user);

        return new UserResponseDto
        {
            Id = user.Id,
            Email = user.Email,
            FullName = $"{user.Firstname} {user.Lastname}",
            ClientNumber = user.ClientNumber,
            Type = user.Type,
            Phonenumber = user.Phonenumber
        };
    }
    public async Task<bool> ChangeStatusAsync(int id, bool status)
    {
        var user = await _userRepository.GetByIdAsync(id);
        if (user == null) return false;

        user.Status = status;
        await _userRepository.UpdateAsync(user);
        return true;
    }

    public async Task<bool> DeleteUserAsync(int id)
    {
        var user = await _userRepository.GetByIdAsync(id);
        if (user == null) return false;

        await _userRepository.RemoveAsync(user);
        return true;
    }

    public async Task<IEnumerable<UserResponseDto>> GetActiveUsersAsync()
    {
        var users = await _userRepository.GetActiveAsync();
        return users.Select(user => new UserResponseDto
        {
            Id = user.Id,
            Email = user.Email,
            FullName = $"{user.Firstname} {user.Lastname}",
            ClientNumber = user.ClientNumber,
            Type = user.Type,
            Phonenumber = user.Phonenumber
        });
    }
}