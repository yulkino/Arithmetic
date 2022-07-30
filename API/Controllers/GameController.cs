using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route(("controller"))]
public class GameController : ControllerBase
{
    public GameController()
    {
        
    }

    [HttpPost("User/{userId}/Game")]
    public ActionResult StartGame()
    {

    }

    [HttpGet("User/{userId}/Game/{id}")]
    public ActionResult GetFirstExercise()
    {

    }

    [HttpPost("User/{userId}/Game/{id}")]
    public ActionResult ProcessGameProgress()
    {

    }
}
