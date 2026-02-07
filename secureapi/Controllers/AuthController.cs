using Microsoft.AspNetCore.Mvc;
using MyApi.Api.Dtos;
using MyApi.Api.Services;

namespace MyApi.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly AuthService _auth;

    public AuthController(AuthService auth) => _auth = auth;

    [HttpPost("login")]
    public ActionResult<LoginResponse> Login([FromBody] LoginRequest req)
    {
        var (ok, token) = _auth.Login(req.Username, req.Password);
        if (!ok) return Unauthorized(new { message = "Invalid username or password" });

        return Ok(new LoginResponse(token!));
    }
}
