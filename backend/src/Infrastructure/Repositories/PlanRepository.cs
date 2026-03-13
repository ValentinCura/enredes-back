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
                .Include(p => p.PlanLocalities)
                    .ThenInclude(pl => pl.Locality)
                .Where(p => p.PlanLocalities.Any(pl => pl.LocalityId == localityId) && p.Status == true)
                .ToListAsync();
        }

        public override async Task<List<Plan>> GetAllAsync()
        {
            return await _context.Plans
                .Include(p => p.PlanLocalities)
                    .ThenInclude(pl => pl.Locality)
                .ToListAsync();
        }

        public async Task<Plan?> GetByIdWithLocalitiesAsync(int id)
        {
            return await _context.Plans
                .Include(p => p.PlanLocalities)
                .FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}