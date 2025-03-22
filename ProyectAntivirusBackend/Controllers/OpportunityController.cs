using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectAntivirusBackend.Data;
using ProyectAntivirusBackend.DTOs;
using ProyectAntivirusBackend.Models;

namespace ProyectAntivirusBackend.Controllers
{
    [Route("api/v1/opportunity")]
    [ApiController]
    public class OpportunityController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public OpportunityController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/v1/opportunity
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OpportunityDTO>>> GetOpportunity()
        {
            var opportunities = await _context.Opportunities
                .Include(o => o.OpportunityTypes)
                    .ThenInclude(ot=>ot.Categories)
                .Include(o => o.Sectors)
                .Include(o => o.Institutions)
                .Include(o => o.Localities)
                .ToListAsync();

            if (!opportunities.Any()) return NotFound();

            var opportunitiesDTO = _mapper.Map<List<OpportunityDTO>>(opportunities); // Mapear lista completa

            return Ok(opportunitiesDTO);
        }

        // GET: api/v1/opportunity/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OpportunityDTO>> GetOpportunity(int id)
        {
            var opportunity = await _context.Opportunities
                .Include(o => o.OpportunityTypes)
                    .ThenInclude(ot=>ot.Categories)
                .Include(o => o.Sectors)
                .Include(o => o.Institutions)
                .Include(o => o.Localities)
                .FirstOrDefaultAsync(o => o.Id == id);
            if (opportunity == null) return NotFound();

            var opportunityDTO = _mapper.Map<OpportunityDTO>(opportunity);
            return Ok(opportunityDTO);
        }

        // POST: api/v1/opportunity
        [HttpPost]
        public async Task<ActionResult<OpportunityDTO>> PostOpportunity([FromBody] CreateOpportunityDTO createOpportunityDTO)
        {
            Console.WriteLine($"📌 SectorId recibido: {createOpportunityDTO.SectorId}");
            Console.WriteLine($"📌 InstitutionId recibido: {createOpportunityDTO.InstitutionId}");
            Console.WriteLine($"📌 OpportunityTypeId recibido: {createOpportunityDTO.OpportunityTypeId}");
            Console.WriteLine($"📌 LocalityId recibido: {createOpportunityDTO.LocalityId}");

            // Validaciones de entidades relacionadas
            var sector = await _context.Sectors.FindAsync(createOpportunityDTO.SectorId);
            if (sector == null)
                return BadRequest("❌ Error: Sector inválido. Debe existir en la base de datos.");

            var institution = await _context.Institutions.FindAsync(createOpportunityDTO.InstitutionId);
            if (institution == null)
                return BadRequest("❌ Error: Institución inválida. Debe existir en la base de datos.");

            var opportunityType = await _context.OpportunityTypes.FindAsync(createOpportunityDTO.OpportunityTypeId);
            if (opportunityType == null)
                return BadRequest("❌ Error: Tipo de oportunidad inválido.");

            var locality = await _context.Localities.FindAsync(createOpportunityDTO.LocalityId);

            // Creación del objeto Opportunity
            var opportunity = new Opportunity
            {
                Title = createOpportunityDTO.Title,
                Description = createOpportunityDTO.Description,
                SectorId = createOpportunityDTO.SectorId,
                InstitutionId = createOpportunityDTO.InstitutionId,
                OpportunityTypeId = createOpportunityDTO.OpportunityTypeId,
                LocalityId = createOpportunityDTO.LocalityId,
                Requirements = createOpportunityDTO.Requirements,
                Benefits = createOpportunityDTO.Benefits,
                Modality = createOpportunityDTO.Modality,
                PublicationDate = DateTime.UtcNow,
                ExpirationDate = createOpportunityDTO.ExpirationDate,
                Status = createOpportunityDTO.Status,

                Sectors = sector,
                Institutions = institution,
                OpportunityTypes = opportunityType,
                Localities = locality
            };


            _context.Opportunities.Add(opportunity);
            await _context.SaveChangesAsync();

            var opportunityDTO = _mapper.Map<OpportunityDTO>(opportunity);

            return CreatedAtAction(nameof(GetOpportunity), new { id = opportunity.Id }, opportunityDTO);
        }

        [HttpPost("{id}/rate")]
        public async Task<IActionResult> RateOpportunity(int id, [FromBody] RatingRequest request)
        {
            var userId = GetUserIdFromToken(); // Obtener el usuario autenticado
            var existingRating = await _context.Ratings
                .FirstOrDefaultAsync(r => r.UserId == userId && r.OpportunityId == id);

            if (existingRating != null)
            {
                // Si ya votó, actualizar su puntuación y comentario
                existingRating.Score = (int)request.Score;
                existingRating.Comment = (string)request.Comment;
                await _context.SaveChangesAsync();
            }
            else
            {
                // Si no ha votado, registrar la calificación
                var rating = new Rating
                {
                    UserId = userId,
                    OpportunityId = id,
                    Score = (int)request.Score,
                    Comment = (string)request.Comment
                };
                _context.Ratings.Add(rating);
                await _context.SaveChangesAsync();
            }

            // Recalcular promedio de calificaciones
            var averageRating = await _context.Ratings
                .Where(r => r.OpportunityId == id)
                .AverageAsync(r => r.Score);

            return Ok(new { newRating = averageRating });
        }

        private int GetUserIdFromToken()
        {
            throw new NotImplementedException();
        }


        // PUT: api/v1/opportunity/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOpportunity(int id, [FromBody] CreateOpportunityDTO createOpportunityDTO)
        {
            var opportunity = await _context.Opportunities.FindAsync(id);
            if (opportunity == null) return NotFound();

            _mapper.Map(createOpportunityDTO, opportunity);
            _context.Entry(opportunity).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/v1/opportunity/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOpportunity(int id)
        {
            var opportunity = await _context.Opportunities.FindAsync(id);
            if (opportunity == null) return NotFound();

            _context.Opportunities.Remove(opportunity);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }

    public class RatingRequest
    {
        public object Score { get; internal set; }
        public object Comment { get; internal set; }
    }
}
