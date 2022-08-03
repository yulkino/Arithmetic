using API.DTOs.ResultDtos;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("controller")]
public class ResultController : ControllerBase
{
    public ResultController()
    {
        
    }

    [HttpGet("User/{userId}/Game/{gameId}/Result")]
    public ActionResult<ResultDto> GetGameResult([FromRoute] Guid userId, [FromRoute] Guid gameId)
    {

    }
}
