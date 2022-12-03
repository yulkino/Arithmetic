using API.DTOs.UserDtos;
using Application.Mediators.UserMediator.Add;
using Application.Mediators.UserMediator.Get;
using AutoMapper;
using Domain.Entity;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public sealed class UserController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public UserController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpPost("login")]
    public async Task<ActionResult<UserDto>> Login([FromBody] LoginDto loginData, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new GetUserQuery(loginData.Login, loginData.PasswordHash), cancellationToken);
        //TODO error catch
        var result = _mapper.Map<User, UserDto>(response.Value);
        return result;
    }

    [HttpPost("register")]
    public async Task<ActionResult<UserDto>> Register([FromBody] RegisterDto registerData, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(
            new AddUserCommand(registerData.Login, registerData.PasswordHash, registerData.PasswordHashConfirmation), 
            cancellationToken);
        //TODO error catch
        var result = _mapper.Map<User, UserDto>(response.Value);
        return result;
    }
}
