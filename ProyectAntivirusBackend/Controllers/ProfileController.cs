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

            var profile = new Profile
            {
                Name = creatProfileDTO.Name,
                Email = createProfileDTO.Email,
                Phone = createProfileDTO.Phone,
            };

            _context.Profile.Add(profile);
            await _context.SaveChangesAsync();

            var ProfileDTO = new ProfileDTO
            {
                Id = profile.Id,
                Name = profile.Name,
                Email = profile.Email,
                Phone = profile.Phone,
            };

            return CreatedAtAction(nameof(GetProfile), new { id = profile.Id }, profileDTO);

        }

        // PUT: api/v1/profile/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProfile(Guid id, CreateProfileDTO createProfileDTO)
        {
            var profile = await _context.Profiles.FindAsync(id);
            if (profile == null) return NotFound();

            _mapper.Map(createProfileDTO, profile);
            _context.Entry(profile).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/v1/profile/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProfile(Guid id)
        {
            var profile = await _context.Profiles.FindAsync(id);
            if (profile == null) return NotFound();

            _context.Profiles.Remove(profile);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}