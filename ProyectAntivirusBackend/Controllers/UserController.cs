using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProyectAntivirusBackend.Services;
using Microsoft.EntityFrameworkCore;
using ProyectAntivirusBackend.Data;
using ProyectAntivirusBackend.DTOs; // Asegúrate de que este using esté presente
using ProyectAntivirusBackend.Models;
using Microsoft.AspNetCore.Authorization;

namespace ProyectAntivirusBackend.Controllers
{
    [Route("api/v1/user")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly JwtService _jwtService;

        public UsersController(ApplicationDbContext context, IMapper mapper, JwtService jwtService)
        {
            _context = context;
            _mapper = mapper;
            _jwtService = jwtService; // Inyectar JwtService
        }

        // POST: api/v1/user/login
        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(LoginDTO loginDTO)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == loginDTO.Email);

            if (user == null || !BCrypt.Net.BCrypt.Verify(loginDTO.Password, user.PasswordHash))
                return Unauthorized("Credenciales inválidas");

            var token = _jwtService.GenerateToken(user.Email, user.Role);
            return Ok(new { Token = token });
        }

        // GET: api/v1/user
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetUsers()
        {
            var users = await _context.Users.ToListAsync();
            var userDTOs = _mapper.Map<IEnumerable<UserDTO>>(users);
            return Ok(userDTOs);
        }

        // GET: api/v1/user/5
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDTO>> GetUser(Guid id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return NotFound();

            var userDTO = _mapper.Map<UserDTO>(user);
            return Ok(userDTO);
        }

        // POST: api/v1/user
        [HttpPost]
        public async Task<ActionResult<UserDTO>> PostUser(CreateUserDTO createUserDTO)
        {
            var user = _mapper.Map<User>(createUserDTO);
            user.RegistrationDate = DateTime.UtcNow;
            user.IsActive = true;

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            var userDTO = _mapper.Map<UserDTO>(user);
            return CreatedAtAction(nameof(GetUser), new { id = user.Id }, userDTO);
        }

        // PUT: api/v1/user/5
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(Guid id, CreateUserDTO createUserDTO)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return NotFound();

            _mapper.Map(createUserDTO, user);
            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/v1/user/5
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return NotFound();

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}