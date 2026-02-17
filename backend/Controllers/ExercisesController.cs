using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using backend.Application.Services;
using backend.Application.DTOs;
using Microsoft.VisualBasic;

namespace backend.Controllers;

[ApiController]
[Route("api/exercises")]
[Authorize]
public class ExercisesController : BaseApiController
{
    private readonly ExerciseService _service;
    public ExercisesController(ExerciseService service) => _service = service;

    // GET /api/exercises
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var userId = GetUserId();
        var result = await _service.GetAllByUserIdAsync(userId);
        return Ok(result);
    }

    // GET /api/exercises/{id}
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var userId = GetUserId();
        var exercise = await _service.GetByIdAsync(userId, id);
        if (exercise == null)
            return NotFound(new { message = "Exercise not found" });

        return Ok(exercise);
    }

    // POST /api/exercises
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateExerciseDTO dto)
    {
        var userId = GetUserId();
        var exercise = await _service.CreateAsync(userId, dto);
        return CreatedAtAction(
            nameof(GetById),
            new { id = exercise.Id },
            exercise
        );
    }

    // PUT /api/exercises/{id}
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateExerciseDTO dto)
    {
        var userId = GetUserId();
        var updated = await _service.UpdateAsync(userId, id, dto);
        if (updated == null)
            return NotFound(new { message = "Exercise not found" });
        return Ok(updated);
    }

    // DELETE /api/exercises/{id}
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var userId = GetUserId();
        await _service.DeleteAsync(userId, id);
        return NoContent();
    }
}
