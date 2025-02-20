using Microsoft.EntityFrameworkCore;
using ProyectAntivirusBackend.Data;
using ProyectAntivirusBackend.Migrations;
using ProyectAntivirusBackend.Models;

namespace ProyectAntivirusBackend.Service
{
    public interface IInstitutionService1
    {
        Task AddAsync(Institution institution);
        Task DeleteByIdAsync(int id);
        Task<IEnumerable<Institution>> GetAllAsync();
        Task<Institution?> GetByIdAsync(int id);
        Task<List<Institution>> GetInstitutionsAsync();
        Task UpdateAsync(Institution institution);
    }
}