using Application.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class LocalityRepository : BaseRepository<Locality>, ILocalityRepository
    {
        public LocalityRepository(ApplicationDbContext context) : base(context) { }

        public async Task<Locality?> GetByCodeAsync(string cod)
        {
            return await _context.Localities
                .FirstOrDefaultAsync(l => l.Cod == cod);
        }
    }
}