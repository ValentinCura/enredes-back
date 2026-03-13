using Application.Interfaces; // Asegurate de que el namespace de la base sea correcto
using Domain.Entities;

namespace Application.Interfaces;

public interface IUserRepository : IBaseRepository<User>
{
    // Aquí agregamos comportamientos que NO son genéricos
    // Por ejemplo, buscar por email para el Login o verificar si un número de cliente existe
    Task<User?> GetByEmailAsync(string email);
    Task<bool> ExistsByEmailAsync(string email);
    Task<User?> GetByResetTokenAsync(string token);
}