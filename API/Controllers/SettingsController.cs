using API.DTOs.SettingsDtos;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
public class SettingsController : ControllerBase
{
    public SettingsController()
    {

    }

    [HttpGet("User/{userId}/Settings")]
    public ActionResult<SettingsDto> GetUserSetting([FromRoute] Guid userId)
    {
        throw new InvalidOperationException();
    }

    [HttpPut("User/{userId}/Settings")]
    public ActionResult<SettingsDto> EditUserSettings([FromRoute] Guid usrId, [FromBody] SettingsDto settings)
    {
        throw new InvalidOperationException();
    }

}
