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
            Phonenumber = user.Phonenumber
        });
    }
}