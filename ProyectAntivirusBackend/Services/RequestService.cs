using ProyectAntivirusBackend.Repositories;
using ProyectAntivirusBackend.Models;
using ProyectAntivirusBackend.DTOs;

public class RequestService : IRequestService
{
    private readonly IRequestRepository _repository;
    public RequestService(IRequestRepository repository) { _repository = repository; }
    public async Task<IEnumerable<RequestDTO>> GetAllAsync() { return (await _repository.GetAllAsync()).Select(r => new RequestDTO { Id = r.Id, UserId = r.UserId, OpportunityId = r.OpportunityId, State = r.State, RequestDate = r.RequestDate }); }
    public async Task<RequestDTO> GetByIdAsync(int id) { var r = await _repository.GetByIdAsync(id); return new RequestDTO { Id = r.Id, UserId = r.UserId, OpportunityId = r.OpportunityId, State = r.State, RequestDate = r.RequestDate }; }
    public async Task AddAsync(RequestDTO dto) { var r = new Request { UserId = dto.UserId, OpportunityId = dto.OpportunityId, State = dto.State, RequestDate = dto.RequestDate }; await _repository.AddAsync(r); }
    public async Task UpdateAsync(RequestDTO dto) { var r = new Request { Id = dto.Id, UserId = dto.UserId, OpportunityId = dto.OpportunityId, State = dto.State, RequestDate = dto.RequestDate }; await _repository.UpdateAsync(r); }
    public async Task DeleteAsync(int id) => await _repository.DeleteAsync(id);

    Task<object?> IRequestService.GetAllAsync()
    {
        throw new NotImplementedException();
    }

    Task<object?> IRequestService.GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }
}