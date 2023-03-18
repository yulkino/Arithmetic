using API.DTOs.SettingsDtos.EditSettingsDtos;
using API.DTOs.SettingsDtos.GetSettingsDtos;
using Application.Mediators.DifficultyMediator;
using Application.Mediators.OperationMediator;
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
    public async Task<ActionResult<SettingsDto>> EditUserSettings([FromRoute] Guid userId, [FromBody] EditSettingsDto settings,
        CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(
                new EditSettingsCommand(
                    userId,
                    _mapper.Map<List<OperationIdDto>, List<OperationIdItemDto>>(settings.Operations),
                    _mapper.Map<DifficultyIdDto, DifficultyIdItemDto>(settings.Difficulty),
                    settings.ExerciseCount),
                cancellationToken);
        //TODO error catch
        var result = _mapper.Map<Settings, SettingsDto>(response.Value);
        return result;
    }

    [HttpGet("Settings/Operations")]
    public async Task<ActionResult<List<OperationDto>>> GetOperationsList(CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new GetOperationsQuery(), cancellationToken);
        //TODO error catch
        var result = _mapper.Map<List<Operation>, List<OperationDto>>(response.Value);
        return result;
    }

    [HttpGet("Settings/Difficulties")]
    public async Task<ActionResult<List<DifficultyDto>>> GetDifficultiesList(CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new GetDifficultiesQuery(), cancellationToken);
        //TODO error catch
        var result = _mapper.Map<List<Difficulty>, List<DifficultyDto>>(response.Value);
        return result;
    }
}
