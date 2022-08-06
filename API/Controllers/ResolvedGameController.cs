using API.DTOs.ResolvedGameDtos;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
public sealed class ResolvedGameController : ControllerBase
{
    public ResolvedGameController()
    {
        
    }

    [HttpGet("User/{userId}/Game/{gameId}/Result")]
    public ActionResult<ResolvedGameDto> GetGameResult([FromRoute] Guid userId, [FromRoute] Guid gameId)
    {
        throw new InvalidOperationException();
    }
}
