using API.DTOs.UserDtos;
using Application.ClientErrors.ErrorCodes;
using Application.Mediators.UserMediator.Add;
using Application.Mediators.UserMediator.Get;
using AutoMapper;
using Domain.Entity;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public sealed class UserController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public UserController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpPost("login")]
    [ProducesResponseType(Status200OK)]
    [ProducesResponseType(Status400BadRequest)]
    [ProducesResponseType(Status404NotFound)]
    public async Task<IResult> Login([FromBody] LoginDto loginData, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetUserQuery(loginData.Login, loginData.Password), cancellationToken);
        return result.MatchToHttpResponse(
            response => Results.CreatedAtRoute(nameof(Register), _mapper.Map<User, UserDto>(response)),
            error => error.Code switch
            {
                GeneralErrorCodes.Validation => Results.BadRequest(error.Description),
                UserErrorCodes.NotFound => Results.NotFound(error.Description),
                UserErrorCodes.Failure => Results.BadRequest(error.Description),
                _ => throw new InvalidOperationException()
            });
    }

    [HttpPost("register")]
    [ProducesResponseType(Status200OK)]
    [ProducesResponseType(Status400BadRequest)]
    [ProducesResponseType(Status404NotFound)]
    public async Task<IResult> Register([FromBody] RegisterDto registerData,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(
            new AddUserCommand(registerData.Login, registerData.Password, registerData.PasswordConfirmation),
            cancellationToken);
        return result.MatchToHttpResponse(
            response => Results.CreatedAtRoute(nameof(Register), _mapper.Map<User, UserDto>(response)), 
            error => error.Code switch
            {
                GeneralErrorCodes.Validation => Results.BadRequest(error.Description),
                UserErrorCodes.Conflict => Results.Conflict(error.Description),
                _ => throw new InvalidOperationException()
            });
    }
}