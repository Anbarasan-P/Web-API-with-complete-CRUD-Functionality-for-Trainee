using Microsoft.AspNetCore.Mvc;
using Trainee_webAPI.Models;
[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly TraineeRepository _dataAccessLayer;
    private readonly JwtService _jsonWebToken;

    public AuthController(IConfiguration config)
    {
        _dataAccessLayer = new TraineeRepository(config);
        _jsonWebToken = new JwtService(config);
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginModel login)
    {
        var user = _dataAccessLayer.GetByEmail(login.Email);

        if (user == null || user.Password != login.Password)
            return Unauthorized("Invalid credentials");

        var token = _jsonWebToken.GenerateToken(user.Email);
        return Ok(new { Token = token });
    }
}

