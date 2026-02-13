using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using backend.Application.Services;

namespace backend.Controllers;

[ApiController]
[Route("api/muscles")]
[Authorize]
public class MuscleController : ControllerBase
{
    private readonly MuscleService _service;

    public MuscleController(MuscleService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _service.GetAllAsync();
        return Ok(result);
    }
}
