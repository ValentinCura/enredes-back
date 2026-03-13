using Application.Models;

namespace Application.Interfaces
{
    public interface ILocalityService
    {
        Task<LocalityResponseDto> CreateLocalityAsync(LocalityCreateDto dto);
        Task<LocalityResponseDto?> GetLocalityByIdAsync(int id);
        Task<IEnumerable<LocalityResponseDto>> GetAllLocalitiesAsync();
        Task<LocalityResponseDto?> UpdateLocalityAsync(int id, LocalityUpdateDto dto);
        Task<bool> DeleteLocalityAsync(int id);
        Task<IEnumerable<LocalityResponseDto>> GetActiveLocalitiesAsync();
        Task<bool> ChangeStatusAsync(int id, bool status);

    }
}