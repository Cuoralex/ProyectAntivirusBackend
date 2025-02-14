using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectAntivirusBackend.Data;
using ProyectAntivirusBackend.Models;
using ProyectAntivirusBackend.DTOs;

namespace ProyectAntivirusBackend.Controllers
{
    [Route("api/v1/services")]
    [ApiController]
    public class ServicesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ServicesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ServiceDTO>>> GetServices()
        {
            var services = await _context.Services
                .Select(s => new ServiceDTO
                {
                    Id = s.Id,
                    IsActive = s.IsActive,
                    ServiceTypeId = s.ServiceTypeId,
                    Title = s.Title,
                    Description = s.Description,
                    Image = s.Image
                })
                .ToListAsync();

            return Ok(services);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceDTO>> GetService(int id)
        {
            var service = await _context.Services.FindAsync(id);
            if (service == null) return NotFound();

            return Ok(new ServiceDTO
            {
                Id = service.Id,
                IsActive = service.IsActive,
                ServiceTypeId = service.ServiceTypeId,
                Title = service.Title,
                Description = service.Description,
                Image = service.Image
            });
        }

        [HttpPost]
        public async Task<ActionResult<ServiceDTO>> PostService(CreateServiceDTO createServiceDTO)
        {
            var service = new Service
            {
                IsActive = true,
                ServiceTypeId = createServiceDTO.ServiceTypeId,
                Title = createServiceDTO.Title,
                Description = createServiceDTO.Description,
                Image = createServiceDTO.Image
            };

            _context.Services.Add(service);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetService), new { id = service.Id }, new ServiceDTO
            {
                Id = service.Id,
                IsActive = service.IsActive,
                ServiceTypeId = service.ServiceTypeId,
                Title = service.Title,
                Description = service.Description,
                Image = service.Image
            });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutService(int id, CreateServiceDTO createServiceDTO)
        {
            var service = await _context.Services.FindAsync(id);
            if (service == null) return NotFound();

            service.ServiceTypeId = createServiceDTO.ServiceTypeId;
            service.Title = createServiceDTO.Title;
            service.Description = createServiceDTO.Description;
            service.Image = createServiceDTO.Image;

            _context.Entry(service).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteService(int id)
        {
            var service = await _context.Services.FindAsync(id);
            if (service == null) return NotFound();

            _context.Services.Remove(service);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
