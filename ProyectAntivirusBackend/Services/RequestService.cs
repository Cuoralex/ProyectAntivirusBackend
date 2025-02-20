using ProyectAntivirusBackend.Repositories;
using ProyectAntivirusBackend.Models;
using ProyectAntivirusBackend.DTOs;

public class RequestService : IRequestService
{
    private readonly IRequestRepository _repository;

    public RequestService(IRequestRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<RequestDTO>> GetAllAsync()
    {
        var requests = await _repository.GetAllAsync();
        return requests?.Select(r => new RequestDTO
        {
            Id = r.Id,
            UserId = r.UserId,
            OpportunityId = r.OpportunityId,
            State = r.State,
            RequestDate = r.RequestDate
        }) ?? new List<RequestDTO>(); // Evita retornar null
    }

    public async Task<RequestDTO?> GetByIdAsync(int id)
    {
        var r = await _repository.GetByIdAsync(id);
        if (r == null) return null; // Maneja el caso donde no exista la solicitud

        return new RequestDTO
        {
            Id = r.Id,
            UserId = r.UserId,
            OpportunityId = r.OpportunityId,
            State = r.State,
            RequestDate = r.RequestDate
        };
    }

    public async Task AddAsync(RequestDTO dto)
    {
        if (dto == null) throw new ArgumentNullException(nameof(dto));

        var request = new Request
        {
            UserId = dto.UserId,
            OpportunityId = dto.OpportunityId,
            State = dto.State,
            RequestDate = dto.RequestDate
        };

        await _repository.AddAsync(request);
    }

    public async Task UpdateAsync(RequestDTO dto)
    {
        if (dto == null) throw new ArgumentNullException(nameof(dto));

        var existingRequest = await _repository.GetByIdAsync(dto.Id);
        if (existingRequest == null) throw new KeyNotFoundException($"Request con ID {dto.Id} no encontrada.");

        existingRequest.UserId = dto.UserId;
        existingRequest.OpportunityId = dto.OpportunityId;
        existingRequest.State = dto.State;
        existingRequest.RequestDate = dto.RequestDate;

        await _repository.UpdateAsync(existingRequest);
    }

    public async Task DeleteAsync(int id)
    {
        var existingRequest = await _repository.GetByIdAsync(id);
        if (existingRequest == null) throw new KeyNotFoundException($"Request con ID {id} no encontrada.");

        await _repository.DeleteAsync(id);
    }

    Task<object?> IRequestService.GetAllAsync()
    {
        throw new NotImplementedException();
    }

    Task<object?> IRequestService.GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }
}
