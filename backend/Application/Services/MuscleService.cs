using backend.Domain.Repositories;
using backend.Domain.Entities;
using backend.Application.DTOs;

namespace backend.Application.Services;

public class MuscleService
{
    private readonly IMuscleRepository _repository;
    public MuscleService(IMuscleRepository repository) => _repository = repository;

    public async Task<List<MuscleDTO>> GetAllAsync()
    {
        var muscles = await _repository.GetAllAsync();
        return muscles.Select(m => new MuscleDTO
        {
            Id = m.Id,
            Name = m.Name
        }).ToList();
    }

    public async Task<MuscleDTO> CreateAsync(CreateMuscleDTO dto)
    {
        if (await _repository.ExistsByNameAsync(dto.Name))
            throw new InvalidOperationException("Ce muscle existe déjà.");

        var muscle = new Muscle { Name = dto.Name };
        var created = await _repository.AddAsync(muscle);
        return new MuscleDTO { Id = created.Id, Name = created.Name };
    }

    public async Task<MuscleDTO> UpdateAsync(Guid id, UpdateMuscleDTO dto)
    {
        var muscle = await _repository.GetByIdAsync(id) ?? throw new KeyNotFoundException("Muscle non trouvé.");
        if (await _repository.ExistsByNameAsync(dto.Name))
            throw new InvalidOperationException("Ce muscle existe déjà.");

        muscle.Name = dto.Name;
        var updated = await _repository.UpdateAsync(muscle);
        return new MuscleDTO { Id = updated.Id, Name = updated.Name };
    }

    public async Task DeleteAsync(Guid id) => await _repository.DeleteAsync(id);
}
