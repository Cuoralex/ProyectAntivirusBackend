using Microsoft.AspNetCore.Mvc;
using ProyectAntivirusBackend.Services;
using ProyectAntivirusBackend.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Swashbuckle.AspNetCore.Annotations;

namespace ProyectAntivirusBackend.Controllers
{
    [Route("api/v1/opportunity-type")]
    [ApiController]
    public class OpportunityTypeController : ControllerBase
    {
        private readonly OpportunityTypeService _service;

        public OpportunityTypeController(OpportunityTypeService service)
        {
            _service = service;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Get all opportunity types", Description = "Retrieves all opportunity types from the system.")]
        [SwaggerResponse(200, "Returns the list of opportunity types", typeof(IEnumerable<OpportunityType>))]
        public async Task<ActionResult<IEnumerable<OpportunityType>>> GetAll()
        {
            return Ok(await _service.GetAllAsync());
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Get opportunity type by ID", Description = "Retrieves an opportunity type by its ID.")]
        [SwaggerResponse(200, "Returns the opportunity type", typeof(OpportunityType))]
        [SwaggerResponse(404, "Opportunity type not found")]
        public async Task<ActionResult<OpportunityType>> GetById(int id)
        {
            var result = await _service.GetByIdAsync(id);
            return result != null ? Ok(result) : NotFound();
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Create a new opportunity type", Description = "Creates a new opportunity type in the system.")]
        [SwaggerResponse(201, "Opportunity type created successfully", typeof(OpportunityType))]
        [SwaggerResponse(400, "Invalid input provided")]
        public async Task<ActionResult<OpportunityType>> Create([FromBody] OpportunityType opportunityType)
        {
            await _service.AddAsync(opportunityType);
            return CreatedAtAction(nameof(GetById), new { id = opportunityType.Id }, opportunityType);
        }

        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Update an opportunity type", Description = "Updates an existing opportunity type by its ID.")]
        [SwaggerResponse(204, "Opportunity type updated successfully")]
        [SwaggerResponse(400, "ID mismatch or invalid data")]
        public async Task<IActionResult> Update(int id, [FromBody] OpportunityType opportunityType)
        {
            if (id != opportunityType.Id)
                return BadRequest("ID mismatch");

            await _service.UpdateAsync(opportunityType);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Delete an opportunity type", Description = "Deletes an opportunity type by its ID.")]
        [SwaggerResponse(204, "Opportunity type deleted successfully")]
        [SwaggerResponse(404, "Opportunity type not found")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}
