using API.DTOs.SettingsDtos;
using Application.Mediators.SettingsMediator.Edit;
using Application.Mediators.SettingsMediator.Get;
using AutoMapper;
using Domain.Entity.SettingsEntities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
public sealed class SettingsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public SettingsController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpGet("User/{userId}/Settings")]
    public async Task<ActionResult<SettingsDto>> GetUserSetting([FromRoute] Guid userId, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new GetSettingsQuery(userId), cancellationToken);
        //TODO error catch
        var result = _mapper.Map<Settings, SettingsDto>(response.Value);
        return result;
    }

    [HttpPut("User/{userId}/Settings")]
    public async Task<ActionResult<SettingsDto>> EditUserSettings([FromRoute] Guid userId, [FromBody] SettingsDto settings, 
        CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(
                new EditSettingsCommand(userId, settings.Operations, settings.Difficulty, settings.ExerciseCount),
                cancellationToken);
        //TODO error catch
        var result = _mapper.Map<Settings, SettingsDto>(response.Value);
        return result;
    }

}
