using ProyectAntivirusBackend.Models;

namespace ProyectAntivirusBackend.Repositories
{
    public interface IInstitutionRepository
    {
        Task<Institution?> GetByIdAsync(int id);
        Task<IEnumerable<Institution>> GetAllAsync();
        Task AddAsync(Institution institution);
        Task UpdateAsync(Institution institution);
        Task DeleteByIdAsync(int id);
    }
}
