using Microsoft.AspNetCore.Mvc;
using PaymentAPI.Services;
using PaymentAPI.Models;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly ITokenService _tokenService;

    public AuthController(ITokenService tokenService)
    {
        _tokenService = tokenService;
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginRequest request)
    {
        if (!_tokenService.ValidateTestUser(request.Username, request.Password))
            return Unauthorized("Invalid credentials");

        var token = _tokenService.GenerateToken(request.Username);
        return Ok(new { token });
    }
}

