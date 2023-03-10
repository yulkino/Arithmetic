using API.DTOs.GameDtos;
using API.DTOs.ResolvedGameDtos;
using Application.Mediators.GameMediator.Add;
using Application.Mediators.GameMediator.GetExercise;
using Application.Mediators.GameMediator.SaveExercise;
using AutoMapper;
using Domain.Entity.ExerciseEntities;
using Domain.Entity.GameEntities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
public sealed class GameController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public GameController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpPost("User/{userId}/Game")]
    public async Task<ActionResult<GameDto>> StartGame([FromRoute] Guid userId, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new AddGameCommand(userId), cancellationToken);
        //TODO error catch
        var result = _mapper.Map<Game, GameDto>(response.Value);
        return result;
    }

    [HttpGet("User/{userId}/Game/{gameId}")]
    public async Task<ActionResult<ExerciseDto>> GetNextExercise([FromRoute] Guid userId, [FromRoute] Guid gameId, 
        CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new GetExerciseQuery(userId, gameId), cancellationToken);
        //TODO error catch
        var result = _mapper.Map<Exercise, ExerciseDto>(response.Value);
        return result;
    }

    [HttpPost("User/{userId}/Game/{gameId}/Exercise/{exerciseId}")]
    public async Task<ActionResult<ResolvedExerciseDto>> SaveAnswer([FromRoute] Guid userId, [FromRoute] Guid gameId, [FromRoute] Guid exerciseId,
        [FromQuery] double answer, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new SaveExerciseCommand(userId, gameId, exerciseId, answer), cancellationToken);
        //TODO error catch
        var result = _mapper.Map<ResolvedExercise, ResolvedExerciseDto>(response.Value);
        return result;
    }
}
