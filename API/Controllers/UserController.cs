using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("controller")]
public class UserController : ControllerBase
{
    public UserController()
    {

    }

    [HttpGet("login")]
    public ActionResult Login()
    {

    }

    [HttpPost("register")]
    public ActionResult Register()
    {

    }
}
