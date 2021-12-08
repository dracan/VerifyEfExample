using Microsoft.AspNetCore.Mvc;
using VerifyEfExample.Api.Data;

namespace VerifyEfExample.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class BlahController : ControllerBase
{
    private readonly ILogger<BlahController> _logger;
    private readonly DataContext _context;

    public BlahController(ILogger<BlahController> logger, DataContext context)
    {
        _logger = logger;
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> Post()
    {
        _logger.LogInformation("Creating database record...");
        _context.MyTables.Add(new MyTable { Id = Guid.NewGuid(), Name = Guid.NewGuid().ToString() });
        await _context.SaveChangesAsync();
        return StatusCode(201);
    }
}