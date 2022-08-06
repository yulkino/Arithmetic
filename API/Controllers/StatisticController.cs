using API.DTOs.StatisticDtos;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
public class StatisticController : ControllerBase
{
    public StatisticController()
    {
        
    }

    [HttpGet("User/{userId}/Statistic")]
    public ActionResult<StatisticDto> GetStatisticForGame([FromRoute] Guid userId)
    {
        throw new InvalidOperationException();
    }
}
