using Microsoft.AspNetCore.Mvc;
using ProyectAntivirusBackend.Services;
using ProyectAntivirusBackend.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
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
 public async Task<ActionResult<IEnumerable<OpportunityType>>> GetAll()
 {
 return Ok(await _service.GetAllAsync());
 }
 [HttpGet("{id}")]
 public async Task<ActionResult<OpportunityType>> GetById(int id)
 {
 var result = await _service.GetByIdAsync(id);
 return result != null ? Ok(result) : NotFound();
 }
 [HttpPost]
 public async Task<ActionResult<OpportunityType>> Create([FromBody] 
OpportunityType opportunityType)
 {
 await _service.AddAsync(opportunityType);
 return CreatedAtAction(nameof(GetById), new { id = 
opportunityType.Id }, opportunityType);
 }
 [HttpPut("{id}")]
 public async Task<IActionResult> Update(int id, [FromBody] OpportunityType 
opportunityType)
 {
 if (id != opportunityType.Id)
 return BadRequest("ID mismatch");
 await _service.UpdateAsync(opportunityType);
 return NoContent();
 }
 [HttpDelete("{id}")]
 public async Task<IActionResult> Delete(int id)
 {
 await _service.DeleteAsync(id);
 return NoContent();
 }
 }
}