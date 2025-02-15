using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectAntivirusBackend.Data;
using ProyectAntivirusBackend.Models;

[Route("api/[controller]")]
[ApiController]
public class ServiceTypesController : ControllerBase
{
	private readonly ApplicationDbContext _context;

	public ServiceTypesController(ApplicationDbContext context)
	{
		_context = context;
	}

	// GET
	[HttpGet]
	public async Task<ActionResult<IEnumerable<ServiceType>>> GetServicesTypes()
	{
		return await _context.ServicesTypes.ToListAsync();
	}

	// GET
	[HttpGet("{id}")]
	public async Task<ActionResult<ServiceType>> GetServiceType(int id)
	{
		var serviceType = await _context.ServicesTypes.FindAsync(id);

		if (serviceType == null)
		{
			return NotFound();
		}

		return serviceType;
	}

	// POST
	[HttpPost]
	public async Task<ActionResult<ServiceType>> PostServiceType(ServiceType serviceType)
	{
		_context.ServicesTypes.Add(serviceType);
		await _context.SaveChangesAsync();

		return CreatedAtAction(nameof(GetServiceType), new { id = serviceType.Id }, serviceType);
	}

	// PUT
	[HttpPut("{id}")]
	public async Task<IActionResult> PutServiceType(int id, ServiceType serviceType)
	{
		if (id != serviceType.Id)
		{
			return BadRequest();
		}

		_context.Entry(serviceType).State = EntityState.Modified;

		try
		{
			await _context.SaveChangesAsync();
		}
		catch (DbUpdateConcurrencyException)
		{
			if (!ServiceTypeExists(id))
			{
				return NotFound();
			}
			else
			{
				throw;
			}
		}

		return NoContent();
	}

	// DELETE
	[HttpDelete("{id}")]
	public async Task<IActionResult> DeleteServiceType(int id)
	{
		var serviceType = await _context.ServicesTypes.FindAsync(id);
		if (serviceType == null)
		{
			return NotFound();
		}

		_context.ServicesTypes.Remove(serviceType);
		await _context.SaveChangesAsync();

		return NoContent();
	}

	private bool ServiceTypeExists(int id)
	{
		return _context.ServicesTypes.Any(e => e.Id == id);
	}
}
