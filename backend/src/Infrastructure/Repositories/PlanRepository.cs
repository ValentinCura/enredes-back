using Application.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class PlanRepository : BaseRepository<Plan>, IPlanRepository
    {
        public PlanRepository(ApplicationDbContext context) : base(context) { }

        public async Task<List<Plan>> GetByLocalityIdAsync(int localityId)
        {
            return await _context.Plans
                .Where(p => p.LocalityId == localityId)
                .ToListAsync();
        }
        public override async Task<List<Plan>> GetAllAsync()
        {
            return await _context.Plans
                .Include(p => p.Locality)
                .ToListAsync();
        }
    }
}