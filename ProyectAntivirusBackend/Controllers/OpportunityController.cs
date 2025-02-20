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
                .Include(o => o.Sector)
                .Include(o => o.Institution)
                .Include(o => o.OpportunityType)
                .ToListAsync();

            var opportunityDTOs = _mapper.Map<IEnumerable<OpportunityDTO>>(opportunities);
            return Ok(opportunityDTOs);
        }

        // GET: api/v1/opportunity/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OpportunityDTO>> GetOpportunity(int id)
        {
            var opportunity = await _context.Opportunities
                .Include(o => o.Sector)
                .Include(o => o.Institution)
                .Include(o => o.OpportunityType)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (opportunity == null) return NotFound();

            var opportunityDTO = _mapper.Map<OpportunityDTO>(opportunity);
            return Ok(opportunityDTO);
        }

        // POST: api/v1/opportunity
        [HttpPost]
        public async Task<ActionResult<OpportunityDTO>> PostOpportunity(CreateOpportunityDTO createOpportunityDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var institution = await _context.Institutions.FindAsync(createOpportunityDTO.InstitutionId);
            if (institution == null)
                return NotFound(new { message = "Institution not found." });

            var sector = await _context.Sectors.FindAsync(createOpportunityDTO.SectorId);
            if (sector == null)
                return NotFound(new { message = "Sector not found." });

            var opportunityType = await _context.OpportunityTypes.FindAsync(createOpportunityDTO.OpportunityTypeId);
            if (opportunityType == null)
                return NotFound(new { message = "Opportunity Type not found." });

            var opportunity = _mapper.Map<Opportunity>(createOpportunityDTO);
            opportunity.PublicationDate = DateTime.UtcNow;

            _context.Opportunities.Add(opportunity);
            await _context.SaveChangesAsync();

            var opportunityDTO = _mapper.Map<OpportunityDTO>(opportunity);
            return CreatedAtAction(nameof(GetOpportunity), new { id = opportunity.Id }, opportunityDTO);
        }

        // PUT: api/v1/opportunity/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOpportunity(Guid id, CreateOpportunityDTO createOpportunityDTO)
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
        public async Task<IActionResult> DeleteOpportunity(Guid id)
        {
            var opportunity = await _context.Opportunities.FindAsync(id);
            if (opportunity == null) return NotFound();

            _context.Opportunities.Remove(opportunity);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
