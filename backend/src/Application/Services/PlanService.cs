using Application.Interfaces;
using Application.Models;
using Domain.Entities;
namespace Application.Services
{
    public class PlanService : IPlanService
    {
        private readonly IPlanRepository _planRepository;

        public PlanService(IPlanRepository planRepository)
        {
            _planRepository = planRepository;
        }

        public async Task<PlanResponseDto> CreatePlanAsync(PlanCreateDto dto)
        {
            var plan = new Plan
            {
                Name = dto.Name,
                Price = dto.Price,
                Speed = dto.Speed,
                Features = dto.Features ?? new List<string>(),
                Status = true,
                PlanLocalities = dto.LocalityIds.Select(id => new PlanLocality
                {
                    LocalityId = id
                }).ToList()
            };
            await _planRepository.AddAsync(plan);
            return MapToDto(plan);
        }

        public async Task<PlanResponseDto?> GetPlanByIdAsync(int id)
        {
            var plan = await _planRepository.GetByIdAsync(id);
            if (plan == null) return null;
            return MapToDto(plan);
        }

        public async Task<IEnumerable<PlanResponseDto>> GetAllPlansAsync()
        {
            var plans = await _planRepository.GetAllAsync();
            return plans.Select(MapToDto);
        }

        public async Task<IEnumerable<PlanResponseDto>> GetPlansByLocalityIdAsync(int localityId)
        {
            var plans = await _planRepository.GetByLocalityIdAsync(localityId);
            return plans.Select(MapToDto);
        }

        public async Task<PlanResponseDto?> UpdatePlanAsync(int id, PlanUpdateDto dto)
        {
            var plan = await _planRepository.GetByIdWithLocalitiesAsync(id);
            if (plan == null) return null;

            if (dto.Name != null) plan.Name = dto.Name; 
            if (dto.Price != null) plan.Price = dto.Price.Value;
            if (dto.Status != null) plan.Status = dto.Status.Value;
            if (dto.Speed != null) plan.Speed = dto.Speed;
            if (dto.Features != null) plan.Features = dto.Features;
            if (dto.LocalityIds != null)
                plan.PlanLocalities = dto.LocalityIds.Select(localityId => new PlanLocality
                {
                    LocalityId = localityId,
                    PlanId = plan.Id
                }).ToList();

            await _planRepository.UpdateAsync(plan);
            return MapToDto(plan);
        }

        public async Task<bool> DeletePlanAsync(int id)
        {
            var plan = await _planRepository.GetByIdAsync(id);
            if (plan == null) return false;
            await _planRepository.RemoveAsync(plan);
            return true;
        }

        public async Task<IEnumerable<PlanResponseDto>> GetActivePlansAsync()
        {
            var plans = await _planRepository.GetActiveAsync();
            return plans.Select(MapToDto);
        }

        private static PlanResponseDto MapToDto(Plan plan) => new()
        {
            Id = plan.Id,
            Name = plan.Name,
            Price = plan.Price,
            Speed = plan.Speed,
            Status = plan.Status,
            Localities = plan.PlanLocalities
        .Select(pl => new LocalitySimpleDto
        {
            Id = pl.LocalityId,
            Name = pl.Locality?.Name ?? string.Empty
        }).ToList(),
            CreatedAt = plan.CreatedAt
        };
    }
}