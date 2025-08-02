using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Trainee_webAPI.Models;

[ApiController]
[Route("api/[controller]")] // This route will be used for trainee operations
public class TraineeController : ControllerBase
{
    private readonly TraineeRepository dataAccessLayer;

    public TraineeController(IConfiguration config) // Constructor to initialize the data access layer
    {
        dataAccessLayer = new TraineeRepository(config);
    }

    [HttpGet("getall")]
    [Authorize]
    public IActionResult GetAll() // This method requires authorization, allowing only authenticated users to retrieve all trainees
    {
        var trainees = dataAccessLayer.GetAll();
        return Ok(trainees);
    }

    [HttpPost("create")]
    public IActionResult Create([FromBody] Trainee trainee) // This method does not require authorization, allowing anyone to create a trainee
    {
        dataAccessLayer.Create(trainee);
        return Ok("Trainee Created");
    }

    [HttpPut("update")]
    [Authorize]
    public IActionResult Update([FromBody] Trainee trainee) // This method requires authorization, allowing only authenticated users to update a trainee
    {
        var loggedInEmail = User.FindFirstValue(ClaimTypes.Email);
        if (trainee.Email != loggedInEmail)
            return Forbid("You are not allowed to update this record");

        dataAccessLayer.Update(trainee);
        return Ok("Trainee Updated Seccessfully");
    }

    [HttpDelete("delete")]
    [Authorize]
    public IActionResult Delete([FromQuery] string email) // This method requires authorization, allowing only authenticated users to delete a trainee
    {
        var loggedInEmail = User.FindFirstValue(ClaimTypes.Email);
        if (email != loggedInEmail)
            return Forbid("You are not allowed to delete this record");

        dataAccessLayer.Delete(email);
        return Ok("Trainee Deleted Successfully");
    }

    [HttpGet("get")]
    [Authorize]
    public IActionResult GetByEmail([FromQuery] string email) // This method requires authorization, allowing only authenticated users to retrieve a trainee by email
    {
        //var email = User.FindFirstValue(ClaimTypes.Email);
        var trainee = dataAccessLayer.GetByEmail(email);
        return trainee == null ? NotFound() : Ok(trainee);
    }
}
