using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using backend.Application.Services;
using backend.Application.DTOs;

namespace backend.Controllers;

[ApiController]
[Route("api/sessions")]
[Authorize]
public class SessionsController : BaseApiController
{
    private readonly SessionService _service;
    public SessionsController(SessionService service) => _service = service;

    // GET /api/sessions
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var userId = GetUserId();
        var result = await _service.GetAllByUserIdAsync(userId);
        return Ok(result);
    }

    // GET /api/sessions/{id}
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var userId = GetUserId();
        try
        {
            var result = await _service.GetByIdAsync(userId, id);
            return Ok(result);
        }
        catch (KeyNotFoundException e)
        {
            return NotFound(new { message = e.Message });
        }
    }

    // POST /api/sessions
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateSessionDTO dto)
    {
        var userId = GetUserId();
        try
        {
            var result = await _service.CreateAsync(userId, dto);
            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(new { message = e.Message });
        }
    }

    // PUT /api/sessions/{id}
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateSessionDTO dto)
    {
        var userId = GetUserId();
        try
        {
            var result = await _service.UpdateAsync(userId, id, dto);
            return Ok(result);
        }
        catch (KeyNotFoundException e)
        {
            return NotFound(new { message = e.Message });
        }
        catch (Exception e)
        {
            return BadRequest(new { message = e.Message });
        }
    }

    // DELETE /api/sessions/{id}
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var userId = GetUserId();
        try
        {
            await _service.DeleteAsync(userId, id);
            return NoContent();
        }
        catch (KeyNotFoundException e)
        {
            return NotFound(new { message = e.Message });
        }
    }
}