using Application.Interfaces;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface ILocalityRepository : IBaseRepository<Locality>
    {
        Task<Locality?> GetByCodeAsync(string cod);
    }
}