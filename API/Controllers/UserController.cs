using API.DTOs.UserDtos;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public sealed class UserController : ControllerBase
{
    public UserController()
    {

    }

    [HttpPost("login")]
    public ActionResult Login([FromBody] LoginDto loginData)
    {
        throw new InvalidOperationException();
    }

    [HttpPost("register")]
    public ActionResult Register([FromBody] RegisterDto registerData)
    {
        throw new InvalidOperationException();
    }
}
