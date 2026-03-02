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
                Features = dto.Features,
                Colors = dto.Colors,
                Featured = dto.Featured,
                LocalityId = dto.LocalityId
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

        private static PlanResponseDto MapToDto(Plan plan) => new()
        {
            Id = plan.Id,
            Name = plan.Name,
            Price = plan.Price,
            Speed = plan.Speed,
            Features = plan.Features,
            Colors = plan.Colors,
            Featured = plan.Featured,
            LocalityId = plan.LocalityId,
            LocalityName = plan.Locality?.Name ?? string.Empty,
            CreatedAt = plan.CreatedAt
        };
    }
}