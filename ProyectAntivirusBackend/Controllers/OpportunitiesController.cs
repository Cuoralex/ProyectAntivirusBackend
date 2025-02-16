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
    public class OpportunitiesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public OpportunitiesController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/v1/opportunity
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OpportunityDTO>>> GetOpportunities()
        {
            var opportunities = await _context.Opportunities.ToListAsync();
            var opportunityDTOs = _mapper.Map<IEnumerable<OpportunityDTO>>(opportunities);
            return Ok(opportunityDTOs);
        }

        // GET: api/v1/opportunity/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OpportunityDTO>> GetOpportunity(Guid id)
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