using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class RequestsController : ControllerBase
{
    private readonly IRequestService _service;
    public RequestsController(IRequestService service) { _service = service; }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<RequestDto>>> GetAll() => Ok(await _service.GetAllAsync());

    [HttpGet("{id}")]
    public async Task<ActionResult<RequestDto>> GetById(int id) => Ok(await _service.GetByIdAsync(id));

    [HttpPost]
    public async Task<IActionResult> Add(RequestDto dto) { await _service.AddAsync(dto); return CreatedAtAction(nameof(GetById), new { id = dto.Id }, dto); }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, RequestDto dto) { if (id != dto.Id) return BadRequest(); await _service.UpdateAsync(dto); return NoContent(); }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id) { await _service.DeleteAsync(id); return NoContent(); }
}

public interface IRequestService
{
    Task AddAsync(RequestDto dto);
    Task DeleteAsync(int id);
    Task<object?> GetAllAsync();
    Task<object?> GetByIdAsync(int id);
    Task UpdateAsync(RequestDto dto);
}