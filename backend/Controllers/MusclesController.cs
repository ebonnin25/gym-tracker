using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using backend.Application.Services;
using backend.Application.DTOs;

namespace backend.Controllers;

[ApiController]
[Route("api/muscles")]
[Authorize]
public class MusclesController : BaseApiController
{
    private readonly MuscleService _service;
    public MusclesController(MuscleService service) => _service = service;

    // GET /api/muscles
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _service.GetAllAsync();
        return Ok(result);
    }

    // GET /api/muscles/{id}
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var muscle = await _service.GetByIdAsync(id);
        if (muscle == null)
            return NotFound(new { message = "Muscle not found" });
        return Ok(muscle);
    }

    // POST /api/muscles
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateMuscleDTO dto)
    {
        try { return Ok(await _service.CreateAsync(dto)); }
        catch (Exception e) { return BadRequest(new { message = e.Message }); }
    }

    // PUT /api/muscles/{id}
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateMuscleDTO dto)
    {
        try { return Ok(await _service.UpdateAsync(id, dto)); }
        catch (KeyNotFoundException e) { return NotFound(new { message = e.Message }); }
        catch (InvalidOperationException e) { return BadRequest(new { message = e.Message }); }
    }

    // DELETE /api/muscles/{id}
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _service.DeleteAsync(id);
        return NoContent();
    }
}
