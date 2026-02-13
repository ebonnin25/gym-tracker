using backend.Domain.Repositories;
using backend.Application.DTOs;

namespace backend.Application.Services;

public class MuscleService
{
    private readonly IMuscleRepository _repository;

    public MuscleService(IMuscleRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<MuscleDTO>> GetAllAsync()
    {
        var muscles = await _repository.GetAllAsync();
        return muscles.Select(m => new MuscleDTO
        {
            Id = m.Id,
            Name = m.Name
        }).ToList();
    }
}
