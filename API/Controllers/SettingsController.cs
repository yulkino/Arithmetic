using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route(("controller"))]
public class SettingsController : ControllerBase
{
    public SettingsController()
    {

    }

    [HttpGet("User/{id}/Settings")]
    public ActionResult GetUserSetting()
    {

    }

    [HttpPut("User/{userId}/Settings/{id}")]
    public ActionResult EditUserSettings()
    {

    }

}
