using API.DTOs.SettingsDtos.EditSettingsDtos;
using API.DTOs.SettingsDtos.GetSettingsDtos;
using Application.ClientErrors.ErrorCodes;
using Application.Mediators.DifficultyMediator.Get;
using Application.Mediators.OperationMediator.Get;
using Application.Mediators.SettingsMediator.Edit;
using Application.Mediators.SettingsMediator.Get;
using AutoMapper;
using Domain.Entity.SettingsEntities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace API.Controllers;

[ApiController]
[Authorize]
public sealed class SettingsController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public SettingsController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpGet("User/{userId}/Game/{gameId}/Settings")]
    [ProducesResponseType(Status200OK)]
    [ProducesResponseType(Status400BadRequest)]
    [ProducesResponseType(Status404NotFound)]
    public async Task<IResult> GetGameSetting([FromRoute] Guid userId, [FromRoute] Guid gameId,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetSettingsQuery(userId, gameId), cancellationToken);

        return result.MatchToHttpResponse(
            gameSettings => Results.Ok(_mapper.Map<Settings, SettingsDto>(gameSettings)),
            error => error.Code switch
            {
                GeneralErrorCodes.Validation => Results.BadRequest(error.Description),
                UserErrorCodes.NotFound => Results.NotFound(error.Description),
                GameErrorCodes.NotFound => Results.NotFound(error.Description),
                SettingsErrorCodes.NotFound => Results.NotFound(error.Description),
                _ => throw new InvalidOperationException()
            });
    }

    [HttpPut("User/{userId}/Game/{gameId}/Settings")]
    [ProducesResponseType(Status200OK)]
    [ProducesResponseType(Status400BadRequest)]
    [ProducesResponseType(Status404NotFound)]
    [ProducesResponseType(Status409Conflict)]
    public async Task<IResult> EditGameSettings([FromRoute] Guid userId, [FromRoute] Guid gameId,
        [FromBody] EditSettingsDto settings,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new EditSettingsCommand(
                userId,
                gameId,
                settings.OperationIds,
                settings.DifficultyId,
                settings.ExerciseCount),
            cancellationToken);

        return result.MatchToHttpResponse(
            gameSettings => Results.Ok(_mapper.Map<Settings, SettingsDto>(gameSettings)),
            error => error.Code switch
            {
                GeneralErrorCodes.Validation => Results.BadRequest(error.Description),
                UserErrorCodes.NotFound => Results.NotFound(error.Description),
                GameErrorCodes.NotFound => Results.NotFound(error.Description),
                SettingsErrorCodes.NotFound => Results.NotFound(error.Description),
                SettingsErrorCodes.Conflict => Results.Conflict(error.Description),
                SettingsErrorCodes.OperationsErrorCodes.NotFound => Results.NotFound(error.Description),
                SettingsErrorCodes.DifficultyErrorCodes.NotFound => Results.NotFound(error.Description),
                _ => throw new InvalidOperationException()
            });
    }

    [HttpGet("Settings/Operations")]
    [ProducesResponseType(Status200OK)]
    public async Task<IResult> GetOperations(CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetOperationsQuery(), cancellationToken);

        return result.MatchToHttpResponse(
            operations => Results.Ok(_mapper.Map<HashSet<Operation>, HashSet<OperationDto>>(operations)),
            _ => throw new InvalidOperationException());
    }

    [HttpGet("Settings/Difficulties")]
    [ProducesResponseType(Status200OK)]
    public async Task<IResult> GetDifficulties(CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetDifficultiesQuery(), cancellationToken);

        return result.MatchToHttpResponse(
            difficulties => Results.Ok(_mapper.Map<HashSet<Difficulty>, HashSet<DifficultyDto>>(difficulties)),
            _ => throw new InvalidOperationException());
    }
}