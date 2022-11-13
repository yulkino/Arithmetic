using API.DTOs.GameDtos;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
public sealed class GameController : ControllerBase
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
    public ActionResult<ExerciseDto> GetNextExercise([FromRoute] Guid userId, [FromRoute] Guid gameId)
    {
        throw new InvalidOperationException();
    }

    [HttpPost("User/{userId}/Game/{gameId}/Exercise/{exerciseId}")]
    public ActionResult<ExerciseDto> SaveAnswer([FromRoute] Guid userId, [FromRoute] Guid gameId, [FromQuery] double answer)
    {
        throw new InvalidOperationException();
    }
}
