using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectAntivirusBackend.Data;
using ProyectAntivirusBackend.DTOs;
using ProyectAntivirusBackend.Models;
using ProfileModel = ProyectAntivirusBackend.Models.Profile;

namespace ProyectAntivirusBackend.Controllers
{
    [Route("api/v1/profile")]
    [ApiController]
    public class ProfilesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ProfilesController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/v1/profile
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProfileDTO>>> GetProfiles()
        {
            var profiles = await _context.Profiles.ToListAsync();
            var profileDTOs = _mapper.Map<IEnumerable<ProfileDTO>>(profiles);
            return Ok(profileDTOs);
        }

        // GET: api/v1/profile/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProfileDTO>> GetProfile(Guid id)
        {
            var profile = await _context.Profiles.FindAsync(id);
            if (profile == null) return NotFound();

            var profileDTO = _mapper.Map<ProfileDTO>(profile);
            return Ok(profileDTO);
        }

        // POST: api/v1/profile
        [HttpPost]
        public async Task<ActionResult<ProfileDTO>> PostProfile(CreateProfileDTO createProfileDTO)
        {

            var user = await _context.Users.FindAsync(createProfileDTO.UserId);
            if (user == null)
            {
                return BadRequest("El usuario no existe.");
            }

            var profile = new ProfileModel
            {
                UserId = user.Id,
                User = user,
                Preferences = "{}",
                Biography = "",
                ProfilePicture = ""
            };


            _context.Profiles.Add(profile);
            await _context.SaveChangesAsync();

            var profileDTO = new ProfileDTO
            {
                Id = profile.Id,
                UserId = profile.UserId,
                Preferences = profile.Preferences,
                Biography = profile.Biography,
                ProfilePicture = profile.ProfilePicture,
                Name = profile.User.Name,
                Email = profile.User.Email,
                Phone = profile.User?.Phone ?? string.Empty
            };

            return CreatedAtAction(nameof(GetProfile), new { id = profile.Id }, profileDTO);

            // PUT: api/v1/profile/5
#pragma warning disable CS8321 // Local function is declared but never used
            [HttpPut("{id}")]
            async Task<IActionResult> PutProfile(Guid id, CreateProfileDTO createProfileDTO)
            {
                var profile = await _context.Profiles.FindAsync(id);
                if (profile == null) return NotFound();

                _mapper.Map(createProfileDTO, profile);
                _context.Entry(profile).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return NoContent();
            }
#pragma warning restore CS8321 // Local function is declared but never used

            // DELETE: api/v1/profile/5
#pragma warning disable CS8321 // Local function is declared but never used
            [HttpDelete("{id}")]
            async Task<IActionResult> DeleteProfile(Guid id)
            {
                var profile = await _context.Profiles.FindAsync(id);
                if (profile == null) return NotFound();

                _context.Profiles.Remove(profile);
                await _context.SaveChangesAsync();
                return NoContent();
            }
#pragma warning restore CS8321 // Local function is declared but never used
        }
    }
}