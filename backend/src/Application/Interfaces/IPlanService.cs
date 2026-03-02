using Application.Models;

namespace Application.Interfaces
{
    public interface IPlanService
    {
        Task<PlanResponseDto> CreatePlanAsync(PlanCreateDto dto);
        Task<PlanResponseDto?> GetPlanByIdAsync(int id);
        Task<IEnumerable<PlanResponseDto>> GetAllPlansAsync();
        Task<IEnumerable<PlanResponseDto>> GetPlansByLocalityIdAsync(int localityId);
    }
}