using Application.Models;

namespace Application.Interfaces
{
    public interface ILocalityService
    {
        Task<LocalityResponseDto> CreateLocalityAsync(LocalityCreateDto dto);
        Task<LocalityResponseDto?> GetLocalityByIdAsync(int id);
        Task<IEnumerable<LocalityResponseDto>> GetAllLocalitiesAsync();
    }
}