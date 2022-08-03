using API.DTOs.StatisticDtos;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("controller")]
public class StatisticController : ControllerBase
{
    public StatisticController()
    {
        
    }

    [HttpGet("User/{userId}/Statistic")]
    public ActionResult<StatisticDto> GetStatisticForGame([FromRoute] Guid userId)
    {

    }
}
