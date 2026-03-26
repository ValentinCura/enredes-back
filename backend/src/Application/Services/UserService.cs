using Application.Models;
using Application.Interfaces;
using Domain.Entities;
using BCrypt.Net;
namespace Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IMailService _mailService;
    public UserService(IUserRepository userRepository, IMailService mailService)
    {
        _userRepository = userRepository;
        _mailService = mailService;
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
            Phonenumber = userDto.Phonenumber,
            Type = "Client"
        };

        await _userRepository.AddAsync(user);
        // Al hacer SaveChanges (dentro del add), EF Core actualizará el objeto 'user' con el ID numérico generado.

        return new UserResponseDto
        {
            Id = user.Id, // Aquí 'Id' ya tendrá el valor 1, 2, 3...
            Email = user.Email,
            FirstName = user.Firstname,
            LastName = user.Lastname,
            Type = user.Type,
            Phonenumber = userDto.Phonenumber
        };
    }
    public async Task<UserResponseDto> CreateAdminAsync(UserCreateDto dto)
    {
        // Verificar si el email ya existe
        var existingUser = await _userRepository.GetByEmailAsync(dto.Email);
        if (existingUser != null)
            throw new InvalidOperationException("El email ya está registrado");

        var admin = new User
        {
            Email = dto.Email,
            Password = BCrypt.Net.BCrypt.HashPassword(dto.Password),
            Firstname = dto.FirstName,
            Lastname = dto.LastName,
            Phonenumber = dto.Phonenumber,
            Type = "Admin", // ← Aquí defines que es Admin
            Status = true
        };

        await _userRepository.AddAsync(admin);

        return new UserResponseDto
        {
            Id = admin.Id,
            Email = admin.Email,
            FirstName = admin.Firstname,
            LastName = admin.Lastname,
            Type = admin.Type,
            Phonenumber = admin.Phonenumber
        };
    }
    public async Task<bool> AdminResetPasswordAsync(int id, string newPassword)
    {
        var user = await _userRepository.GetByIdAsync(id);
        if (user == null) return false;

        user.Password = BCrypt.Net.BCrypt.HashPassword(newPassword);
        await _userRepository.UpdateAsync(user);
        return true;
    }


    public async Task<UserResponseDto?> GetUserByIdAsync(int id)
    {
        var user = await _userRepository.GetByIdAsync(id);
        if (user == null) return null;

        return new UserResponseDto
        {
            Id = user.Id,
            Email = user.Email,
            FirstName = user.Firstname,
            LastName = user.Lastname,
            Phonenumber = user.Phonenumber,
            Type = user.Type
        };
    }
    public async Task<IEnumerable<UserResponseDto>> GetAllUsersAsync()
    {
        // 1. Obtenemos todos los usuarios desde el repositorio (usando el BaseRepository)
        var users = await _userRepository.GetAllAsync();
        var clients = users.Where(u => u.Type == "Client");

        // 2. Mapeamos la lista de Entidades a una lista de DTOs
        return clients.Select(user => new UserResponseDto
        {
            Id = user.Id,
            Email = user.Email,
            FirstName = user.Firstname,
            LastName = user.Lastname,
            Type = user.Type, // O el valor que tengas en la entidad,
            Phonenumber = user.Phonenumber,
            Status = user.Status
        });
    }
    public async Task<UserResponseDto?> UpdateUserAsync(int id, UserUpdateDto dto)
    {
        // Validar que al menos un campo tenga valor
        if (string.IsNullOrWhiteSpace(dto.Email) &&
            string.IsNullOrWhiteSpace(dto.FirstName) &&
            string.IsNullOrWhiteSpace(dto.LastName) &&
            string.IsNullOrWhiteSpace(dto.Phonenumber))
        {
            throw new InvalidOperationException("Debe proporcionar al menos un campo para actualizar");
        }

        var user = await _userRepository.GetByIdAsync(id);
        if (user == null) return null;

        if (dto.Email != null && dto.Email != user.Email)
        {
            var existingUser = await _userRepository.GetByEmailAsync(dto.Email);
            if (existingUser != null)
                throw new ArgumentException("El email ya está registrado");
        }

        if (!string.IsNullOrWhiteSpace(dto.Email))
            user.Email = dto.Email;


        if (!string.IsNullOrWhiteSpace(dto.FirstName))
            user.Firstname = dto.FirstName;

        if (!string.IsNullOrWhiteSpace(dto.LastName))
            user.Lastname = dto.LastName;


        if (!string.IsNullOrWhiteSpace(dto.Phonenumber))
            user.Phonenumber = dto.Phonenumber;

        await _userRepository.UpdateAsync(user);

        return new UserResponseDto
        {
            Id = user.Id,
            Email = user.Email,
            FirstName = user.Firstname,
            LastName = user.Lastname,
            Type = user.Type,
            Phonenumber = user.Phonenumber,
            Status = user.Status
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

    public async Task ForgotPasswordAsync(string email)
    {
        var user = await _userRepository.GetByEmailAsync(email);
        if (user == null) return;

        var token = Guid.NewGuid().ToString();
        user.PasswordResetToken = token;
        user.PasswordResetTokenExpiry = DateTime.UtcNow.AddHours(1);
        await _userRepository.UpdateAsync(user);

        var resetLink = $"https://enredes.vercel.app/reset-password?token={token}";
        var subject = "Restablecer contraseña - Enredes";
        var message = $@"
        <h2>Restablecer contraseña</h2>
        <p>Hacé click en el siguiente link para restablecer tu contraseña:</p>
        <a href='{resetLink}'>Restablecer contraseña</a>
        <p>El link expira en 1 hora.</p>
        <p>Si no solicitaste esto, ignorá este email.</p>
    ";

        _mailService.Send(subject, message, email);
    }

    public async Task ResetPasswordAsync(ResetPasswordDto dto)
    {
        var user = await _userRepository.GetByResetTokenAsync(dto.Token);
        if (user == null)
            throw new ArgumentException("Token inválido");

        if (user.PasswordResetTokenExpiry < DateTime.UtcNow)
            throw new ArgumentException("El token expiró");

        user.Password = BCrypt.Net.BCrypt.HashPassword(dto.NewPassword);
        user.PasswordResetToken = null;
        user.PasswordResetTokenExpiry = null;
        await _userRepository.UpdateAsync(user);
    }


    public async Task<bool> ChangePasswordAsync(int id, ChangePasswordDto dto)
    {
        var user = await _userRepository.GetByIdAsync(id);
        if (user == null) return false;

        if (!BCrypt.Net.BCrypt.Verify(dto.CurrentPassword, user.Password))
            throw new ArgumentException("La contraseña actual es incorrecta");

        user.Password = BCrypt.Net.BCrypt.HashPassword(dto.NewPassword);
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
        var users = await _userRepository.GetAllAsync();
        var clients = users.Where(u => u.Type == "Client");
        return clients.Select(user => new UserResponseDto
        {
            Id = user.Id,
            Email = user.Email,
            FirstName = user.Firstname,
            LastName = user.Lastname,
            Type = user.Type,
            Phonenumber = user.Phonenumber
        });
    }
}