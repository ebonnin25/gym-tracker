using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using backend.Application.Services;
using backend.Application.DTOs;
using System.Security.Claims;

namespace backend.Controllers;

[ApiController]
[Route("api/exercises")]
[Authorize]
public class ExerciseController : ControllerBase
{
    private readonly ExerciseService _service;
    public ExerciseController(ExerciseService service) => _service = service;

    private Guid GetUserId()
    {
        return Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var result = await _service.GetAllByUserIdAsync(userId);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var userId = GetUserId();
        var exercise = await _service.GetByIdAsync(userId, id);

        if (exercise == null)
            return NotFound(new { message = "Exercise not found" });

        return Ok(exercise);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateExerciseDTO dto)
    {
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var exercise = await _service.CreateAsync(userId, dto);
        return CreatedAtAction(
            nameof(GetById),
            new { id = exercise.Id },
            exercise
        );
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateExerciseDTO dto)
    {
        var updated = await _service.UpdateAsync(id, dto);

        if (updated == null)
            return NotFound(new { message = "Exercise not found" });

        return Ok(updated);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var userId = GetUserId();

        await _service.DeleteAsync(id);

        return NoContent();
    }
}
