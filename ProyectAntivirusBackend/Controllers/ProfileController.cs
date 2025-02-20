using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectAntivirusBackend.Data;
using ProyectAntivirusBackend.Models;

namespace ProyectAntivirusBackend.Controllers
{
    [Route("api/v1/profiles")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ProfileController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/v1/profiles/5 (Obtener perfil de usuario por ID de usuario)
        [HttpGet("{userId}")]
        public async Task<ActionResult<Profile>> GetProfile(int userId)
        {
            var profile = await _context.Profiles.FirstOrDefaultAsync(p => p.UserId == userId);

            if (profile == null)
                return NotFound("Perfil no encontrado");

            return Ok(profile);
        }

        // POST: api/v1/profiles (Crear perfil)
        [HttpPost]
        public async Task<ActionResult<Profile>> CreateProfile(Profile profile)
        {
            _context.Profiles.Add(profile);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProfile), new { userId = profile.UserId }, profile);
        }

        // PUT: api/v1/profiles/5 (Actualizar perfil)
        [HttpPut("{userId}")]
        public async Task<IActionResult> UpdateProfile(int userId, Profile profileData)
        {
            var profile = await _context.Profiles.FirstOrDefaultAsync(p => p.UserId == userId);
            if (profile == null) return NotFound();

            profile.Preferences = profileData.Preferences;
            profile.Biography = profileData.Biography;
            profile.ProfileImage = profileData.ProfileImage;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/v1/profiles/5 (Eliminar perfil)
        [HttpDelete("{userId}")]
        public async Task<IActionResult> DeleteProfile(int userId)
        {
            var profile = await _context.Profiles.FirstOrDefaultAsync(p => p.UserId == userId);
            if (profile == null) return NotFound();

            _context.Profiles.Remove(profile);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
