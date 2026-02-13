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

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var result = await _service.GetAllByUserIdAsync(userId);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateExerciseDTO dto)
    {
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var exercise = await _service.CreateAsync(userId, dto);
        return Ok(exercise);
    }
}
