using Microsoft.AspNetCore.Mvc;

namespace Service_1.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AnyControllers : ControllerBase
{
    [HttpPost("Notification")]
    public async Task<IActionResult> Notification([FromBody] int message)
    {
        Console.WriteLine("Good");
        Console.WriteLine(message);
        return Ok();
    }
}