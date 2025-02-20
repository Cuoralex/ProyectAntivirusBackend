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
            var Opportunity = await _context.Opportunities.ToListAsync();
            var opportunityDTOs = _mapper.Map<IEnumerable<OpportunityDTO>>(Opportunity);
            return Ok(opportunityDTOs);
        }

        // GET: api/v1/opportunity/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OpportunityDTO>> GetOpportunity(int id)
        {
            var opportunity = await _context.Opportunities.FindAsync(id);
            if (opportunity == null) return NotFound();

            var opportunityDTO = _mapper.Map<OpportunityDTO>(opportunity);
            return Ok(opportunityDTO);
        }

        // POST: api/v1/opportunity
        [HttpPost]
        public async Task<ActionResult<OpportunityDTO>> PostOpportunity(CreateOpportunityDTO createOpportunityDTO)
        {
            // Buscar el Sector en la base de datos
            var sector = await _context.Sectors.FirstOrDefaultAsync(s => s.Name == createOpportunityDTO.Sector);
            if (sector == null)
            {
                return BadRequest("Sector inválido. Debe ser un sector existente en la base de datos.");
            }

            // Buscar la Institución en la base de datos
            var institution = await _context.Institutions.FirstOrDefaultAsync(i => i.Name == createOpportunityDTO.Institution);
            if (institution == null)
            {
                return BadRequest("Institución inválida. Debe ser una institución existente en la base de datos.");
            }

            // Buscar el Tipo de Oportunidad en la base de datos
            var opportunityType = await _context.OpportunityTypes.FirstOrDefaultAsync(ot => ot.Name == createOpportunityDTO.Type);
            if (opportunityType == null)
            {
                return BadRequest("Tipo de oportunidad inválido.");
            }

            // Mapear la oportunidad sin sector, institution ni type
            var opportunity = _mapper.Map<Opportunity>(createOpportunityDTO);
            opportunity.Sector = sector;  // Asignar Sector desde la BD
            opportunity.Institution = institution;  // Asignar Institution desde la BD
            opportunity.OpportunityType = opportunityType;  // Asignar OpportunityType desde la BD
            opportunity.PublicationDate = DateTime.UtcNow;

            await _context.Opportunities.AddAsync(opportunity);
            await _context.SaveChangesAsync();

            var opportunityDTO = _mapper.Map<OpportunityDTO>(opportunity);
            return CreatedAtAction(nameof(GetOpportunity), new { id = opportunity.Id }, opportunityDTO);
        }



        // PUT: api/v1/opportunity/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOpportunity(int id, CreateOpportunityDTO createOpportunityDTO)
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
}