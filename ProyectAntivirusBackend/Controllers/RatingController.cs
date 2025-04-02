using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectAntivirusBackend.Data;
using ProyectAntivirusBackend.DTOs;
using ProyectAntivirusBackend.Models;

namespace ProyectAntivirusBackend.Controllers
{
    [ApiController]
    [Route("api/v1/ratings")]
    public class RatingsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public RatingsController(ApplicationDbContext context)
        {
            _context = context;
        }

        /// 🔹 Obtener todas las calificaciones
        [HttpGet]
        public async Task<ActionResult<List<Rating>>> GetAllRatings()
        {
            var ratings = await _context.Ratings.Include(r => r.Opportunity).ToListAsync();

            return Ok(ratings);
        }

        /// 🔹 Obtener calificaciones de un usuario específico
        [HttpGet("user/{userId}")]
        public async Task<ActionResult<List<Rating>>> GetUserRatings(int userId)
        {
            var ratings = await _context.Ratings
                .Where(r => r.UserId == userId)
                .Include(r => r.OpportunityId)
                .ToListAsync();

            return Ok(ratings);
        }

        /// 🔹 Obtener una calificación específica por ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Rating>> GetRatingById(int id)
        {
            var rating = await _context.Ratings.FindAsync(id);

            if (rating == null)
            {
                return NotFound(new { message = "Calificación no encontrada" });
            }

            return Ok(rating);
        }

        /// 🔹 Crear una nueva calificación
        [HttpPost("ratings")]
        public async Task<IActionResult> CreateRating([FromBody] Rating request)
        {
            // Validar que el usuario existe
            var user = await _context.Users.FindAsync(request.UserId);
            if (user == null)
            {
                return NotFound(new { message = "Usuario no encontrado" });
            }

            // Validar que la oportunidad existe
            var opportunity = await _context.Opportunities.FindAsync(request.OpportunityId);
            if (opportunity == null)
            {
                return NotFound(new { message = "Oportunidad no encontrada" });
            }

            // Crear la calificación
            var rating = new Rating
            {
                UserId = request.UserId,
                OpportunityId = request.OpportunityId,
                Score = request.Score,
                Comment = request.Comment
            };

            _context.Ratings.Add(rating);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUserRatings), new { userId = request.UserId }, rating);
        }

        /// 🔹 Actualizar una calificación existente
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRating(int id, [FromBody] Rating updatedRating)
        {
            var rating = await _context.Ratings.FindAsync(id);
            if (rating == null)
            {
                return NotFound(new { message = "Calificación no encontrada" });
            }

            if (updatedRating.Score < 1 || updatedRating.Score > 5)
            {
                return BadRequest(new { message = "El puntaje debe estar entre 1 y 5" });
            }

            rating.Score = updatedRating.Score;
            await _context.SaveChangesAsync();

            return Ok(rating);
        }

        /// 🔹 Eliminar una calificación
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRating(int id)
        {
            var rating = await _context.Ratings.FindAsync(id);
            if (rating == null)
            {
                return NotFound(new { message = "Calificación no encontrada" });
            }

            _context.Ratings.Remove(rating);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }

}