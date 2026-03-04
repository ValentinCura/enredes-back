using Application.Models;

namespace Application.Interfaces
{
    public interface IPlanService
    {
        Task<PlanResponseDto> CreatePlanAsync(PlanCreateDto dto);
        Task<PlanResponseDto?> GetPlanByIdAsync(int id);
        Task<IEnumerable<PlanResponseDto>> GetAllPlansAsync();
        Task<IEnumerable<PlanResponseDto>> GetPlansByLocalityIdAsync(int localityId);
        Task<PlanResponseDto?> UpdatePlanAsync(int id, PlanCreateDto dto);
        Task<bool> DeletePlanAsync(int id);
        Task<IEnumerable<PlanResponseDto>> GetActivePlansAsync();
    }
}