using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

    [HttpGet("all")]
    [Authorize]
    public IActionResult GetAll()
    {
        return Ok(dataAccessLayer.GetAll());
    }

    [HttpPost("create")]
    public IActionResult Create([FromBody] Trainee trainee)
    {
        dataAccessLayer.Create(trainee);
        return Ok("Created");
    }

    [HttpPut("update")]
    public IActionResult Update([FromBody] Trainee trainee)
    {
        dataAccessLayer.Update(trainee);
        return Ok("Updated");
    }

    [HttpDelete("delete")]
    public IActionResult Delete([FromQuery] string email)
    {
        dataAccessLayer.Delete(email);
        return Ok("Deleted");
    }

    [HttpGet("get")]
    public IActionResult GetByEmail([FromQuery] string email)
    {
        var trainee = dataAccessLayer.GetByEmail(email);
        if (trainee == null)
        {
            return NotFound("Trainee not found");
        }
        return Ok(trainee);
    }
}
