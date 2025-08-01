using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Trainee_webAPI.Models;

[ApiController]
[Route("api/[controller]")]
public class TraineeController : ControllerBase
{
    private readonly TraineeRepository dataAccessLayer;

    public TraineeController(IConfiguration config)
    {
        dataAccessLayer = new TraineeRepository(config);
    }

    [HttpGet("getall")]
    [Authorize]
    public IActionResult GetAll()
    {
        var trainees = dataAccessLayer.GetAll();
        return Ok(trainees);
    }

    [HttpPost("create")]
    public IActionResult Create([FromBody] Trainee trainee)
    {
        dataAccessLayer.Create(trainee);
        return Ok("Trainee Created");
    }

    [HttpPut("update")]
    [Authorize]
    public IActionResult Update([FromBody] Trainee trainee)
    {
        var loggedInEmail = User.FindFirstValue(ClaimTypes.Email);
        if (trainee.Email != loggedInEmail)
            return Forbid("You are not allowed to update this record");

        dataAccessLayer.Update(trainee);
        return Ok("Trainee Updated Seccessfully");
    }

    [HttpDelete("delete")]
    [Authorize]
    public IActionResult Delete([FromQuery] string email)
    {
        var loggedInEmail = User.FindFirstValue(ClaimTypes.Email);
        if (email != loggedInEmail)
            return Forbid("You are not allowed to delete this record");

        dataAccessLayer.Delete(email);
        return Ok("Trainee Deleted Successfully");
    }

    [HttpGet("get")]
    [Authorize]
    public IActionResult GetByEmail([FromQuery] string email)
    {
        //var email = User.FindFirstValue(ClaimTypes.Email);
        var trainee = dataAccessLayer.GetByEmail(email);
        return trainee == null ? NotFound() : Ok(trainee);
    }
}
