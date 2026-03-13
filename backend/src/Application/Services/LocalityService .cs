using Application.Interfaces;
using Application.Models;
using Domain.Entities;

namespace Application.Services
{
    public class LocalityService : ILocalityService
    {
        private readonly ILocalityRepository _localityRepository;

        public LocalityService(ILocalityRepository localityRepository)
        {
            _localityRepository = localityRepository;
        }

        public async Task<LocalityResponseDto> CreateLocalityAsync(LocalityCreateDto dto)
        {
            var locality = new Locality
            {
                Name = dto.Name,
                Cod = dto.Cod,
                Province = dto.Province,
                Status = dto.Status
            };

            await _localityRepository.AddAsync(locality);

            return MapToDto(locality);
        }
        public async Task<bool> ChangeStatusAsync(int id, bool status)
        {
            var locality = await _localityRepository.GetByIdAsync(id);
            if (locality == null) return false;
            locality.Status = status;
            await _localityRepository.UpdateAsync(locality);
            return true;
        }
        public async Task<LocalityResponseDto?> GetLocalityByIdAsync(int id)
        {
            var locality = await _localityRepository.GetByIdAsync(id);
            if (locality == null) return null;
            return MapToDto(locality);
        }

        public async Task<IEnumerable<LocalityResponseDto>> GetAllLocalitiesAsync()
        {
            var localities = await _localityRepository.GetAllAsync();
            return localities.Select(MapToDto);
        }

        private static LocalityResponseDto MapToDto(Locality locality) => new()
        {
            Id = locality.Id,
            Name = locality.Name,
            Cod = locality.Cod,
            Province = locality.Province,
            Status = locality.Status,
            CreatedAt = locality.CreatedAt
        };
        public async Task<LocalityResponseDto?> UpdateLocalityAsync(int id, LocalityUpdateDto dto)
        {
            var locality = await _localityRepository.GetByIdAsync(id);
            if (locality == null) return null;

            locality.Name = dto.Name;
            locality.Cod = dto.Cod;
            locality.Province = dto.Province;
            locality.Status = dto.Status;

            await _localityRepository.UpdateAsync(locality);
            return MapToDto(locality);
        }

        public async Task<bool> DeleteLocalityAsync(int id)
        {
            var locality = await _localityRepository.GetByIdAsync(id);
            if (locality == null) return false;

            await _localityRepository.RemoveAsync(locality);
            return true;
        }

        public async Task<IEnumerable<LocalityResponseDto>> GetActiveLocalitiesAsync()
        {
            var localities = await _localityRepository.GetActiveAsync();
            return localities.Select(MapToDto);
        }
    }
}