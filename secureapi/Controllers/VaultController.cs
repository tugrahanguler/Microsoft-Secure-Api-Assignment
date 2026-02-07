using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MyApi.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VaultController : ControllerBase
{
    [HttpGet("me")]
    [Authorize]
    public IActionResult Me()
        => Ok(new { user = User.Identity?.Name, roles = User.Claims.Where(c => c.Type.EndsWith("/role")).Select(c => c.Value) });
}
