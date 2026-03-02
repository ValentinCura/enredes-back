using Application.Interfaces;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IPlanRepository : IBaseRepository<Plan>
    {
        Task<List<Plan>> GetByLocalityIdAsync(int localityId);
    }
}