using API.DTOs.GameDtos;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
public class GameController : ControllerBase
{
    public GameController()
    {
        
    }

    [HttpPost("User/{userId}/Game")]
    public ActionResult<GameDto> StartGame([FromRoute] Guid userId)
    {
        throw new InvalidOperationException();
    }

    [HttpGet("User/{userId}/Game/{gameId}")]
    public ActionResult<ExerciseDto> GetFirstExercise([FromRoute] Guid userId, [FromRoute] Guid gameId)
    {
        throw new InvalidOperationException();
    }

    [HttpPost("User/{userId}/Game/{gameId}")]
    public ActionResult<ExerciseDto> ProcessGameProgress([FromRoute] Guid userId, [FromRoute] Guid gameId, [FromQuery] string answer)
    {
        throw new InvalidOperationException();
    }
}
