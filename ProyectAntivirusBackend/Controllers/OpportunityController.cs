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
            var opportunities = await _context.Opportunities.ToListAsync();
            var opportunityDTOs = _mapper.Map<IEnumerable<OpportunityDTO>>(opportunities);
            return Ok(opportunityDTOs);
        }

        // GET: api/v1/opportunity/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OpportunityDTO>> GetOpportunity(int id)
        {
            var opportunity = await _context.Opportunities
                .Include(o => o.OpportunityTypes)
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
            Console.WriteLine($"📌 SectorId recibido en el backend: {createOpportunityDTO.SectorsId}");
            Console.WriteLine($"📌 InstitutionId recibido en el backend: {createOpportunityDTO.InstitutionsId}");
            Console.WriteLine($"📌 OpportunityTypeId recibido en el backend: {createOpportunityDTO.OpportunityTypesId}");
            Console.WriteLine($"📌 LocalitiesId recibido en el backend: {createOpportunityDTO.LocalitiesId}");

            // Buscar entidades en la base de datos
            var sector = await _context.Sectors.FindAsync(createOpportunityDTO.SectorsId);
            if (sector == null) return BadRequest("Error: Sector inválido. Debe ser un sector existente en la base de datos.");

            var institution = await _context.Institutions.FindAsync(createOpportunityDTO.InstitutionsId);
            if (institution == null) return BadRequest("Error: Institución inválida. Debe ser una institución existente en la base de datos.");

            var opportunityType = await _context.Opportunity_Types.FindAsync(createOpportunityDTO.OpportunityTypesId);
            if (opportunityType == null) return BadRequest("Error: Tipo de oportunidad inválido.");

            var locality = await _context.Localities.FindAsync(createOpportunityDTO.LocalitiesId);
            if (locality == null) return BadRequest("Error: Localidad inválida.");

            // Crear la oportunidad con los valores correctos
            var opportunity = new Opportunity
            {
                Title = createOpportunityDTO.Title,
                Description = createOpportunityDTO.Description,
                Sectors = sector,  
                Institutions = institution,  
                OpportunityTypes = opportunityType,  
                Localities = locality,  // Asignación correcta de localidad
                Requirements = createOpportunityDTO.Requirements,
                Benefits = createOpportunityDTO.Benefits,
                Modality = createOpportunityDTO.Modality,
                PublicationDate = DateTime.UtcNow,
                ExpirationDate = createOpportunityDTO.ExpirationDate,
                Status = createOpportunityDTO.Status
            };

            await _context.Opportunities.AddAsync(opportunity);
            await _context.SaveChangesAsync();

            var opportunityDTO = _mapper.Map<OpportunityDTO>(opportunity);
            return CreatedAtAction(nameof(GetOpportunity), new { id = opportunity.Id }, opportunityDTO);
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
}
