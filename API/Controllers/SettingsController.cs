using API.DTOs.SettingsDtos.EditSettingsDtos;
using API.DTOs.SettingsDtos.GetSettingsDtos;
using Application.ClientErrors.ErrorCodes;
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
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public SettingsController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpGet("User/{userId}/Game/{gameId}/Settings")]
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
    public async Task<IResult> GetOperations(CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetOperationsQuery(), cancellationToken);

        return result.MatchToHttpResponse(
            operations => Results.Ok(_mapper.Map<HashSet<Operation>, HashSet<OperationDto>>(operations)),
            _ => throw new InvalidOperationException());
    }

    [HttpGet("Settings/Difficulties")]
    public async Task<IResult> GetDifficulties(CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetDifficultiesQuery(), cancellationToken);

        return result.MatchToHttpResponse(
            difficulties => Results.Ok(_mapper.Map<HashSet<Difficulty>, HashSet<DifficultyDto>>(difficulties)),
            _ => throw new InvalidOperationException());
    }
}