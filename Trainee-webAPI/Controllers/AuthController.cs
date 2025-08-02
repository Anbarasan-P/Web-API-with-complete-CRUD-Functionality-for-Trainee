using Microsoft.AspNetCore.Mvc;
using Trainee_webAPI.Models;

[ApiController]
[Route("api/[controller]")] // This route will be used for authentication
public class AuthController : ControllerBase // ControllerBase is used for API controllers
{
    private readonly TraineeRepository _dataAccessLayer; // Data access layer for trainee operations
    private readonly JwtService _jwtService; // Service for handling JWT operations

    public AuthController(IConfiguration config) // Constructor to initialize the data access layer and JWT service
    {
        _dataAccessLayer = new TraineeRepository(config);
        _jwtService = new JwtService(config);
    }

    [HttpPost("login")] // Route for user login
    public IActionResult Login([FromBody] LoginModel login) // Method to handle user login
    {
        var user = _dataAccessLayer.GetByEmail(login.Email);

        if (user == null || user.Password != login.Password)
            return Unauthorized("Invalid credentials");

        var token = _jwtService.GenerateToken(user.Email);
        return Ok(new { Token = token });
    }
}
