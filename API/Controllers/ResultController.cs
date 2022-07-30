using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("controller")]
public class ResultController : ControllerBase
{
    public ResultController()
    {
        
    }

    [HttpGet("User/{userId}/Game/{id}/Result")]
    public ActionResult GetGameResult()
    {

    }
}
