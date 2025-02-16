using System.Collections.Generic;
using System.Threading.Tasks;
using ProyectAntivirusBackend.Models;

namespace ProyectAntivirusBackend.Repositories
{
    public interface IRequestRepository
    {
        Task<IEnumerable<Request>> GetAllAsync();
        Task<Request?> GetByIdAsync(int id);
        Task<Request> AddAsync(Request request);
        Task<Request?> UpdateAsync(int id, Request request);
        Task<bool> DeleteAsync(int id);
        Task UpdateAsync(Request request); 
    }
}
