using API.DTOs.GameDtos;
using API.DTOs.ResolvedGameDtos;
using Application.ClientErrors.ErrorCodes;
using Application.Mediators.GameMediator.Add;
using Application.Mediators.GameMediator.GetExercise;
using Application.Mediators.GameMediator.SaveExercise;
using AutoMapper;
using Azure;
using Domain.Entity.ExerciseEntities;
using Domain.Entity.GameEntities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
public sealed class GameController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public GameController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpPost("User/{userId}/Game")]
    public async Task<IResult> StartGame([FromRoute] Guid userId, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new AddGameCommand(userId), cancellationToken);

        return result.MatchToHttpResponse(
            game => Results.Ok(_mapper.Map<Game, GameDto>(game)),
            error => error.Code switch
            {
                GeneralErrorCodes.Validation => Results.BadRequest(error.Description),
                UserErrorCodes.NotFound => Results.NotFound(error.Description),
                _ => throw new InvalidOperationException()
            });
    }

    [HttpPost("User/{userId}/Game/{gameId}")]
    public async Task<IResult> GetNextExercise([FromRoute] Guid userId, [FromRoute] Guid gameId,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetExerciseQuery(userId, gameId), cancellationToken);

        return result.MatchToHttpResponse(
            exercise => Results.Ok(_mapper.Map<Exercise, ExerciseDto>(exercise)),
            error => error.Code switch
            {
                GeneralErrorCodes.Validation => Results.BadRequest(error.Description),
                UserErrorCodes.NotFound => Results.NotFound(error.Description),
                GameErrorCodes.NotFound => Results.NotFound(error.Description),
                ExerciseErrorCodes.BeyondAmountSettings => Results.BadRequest(error.Description),
                _ => throw new InvalidOperationException()
            });
    }

    [HttpPost("User/{userId}/Game/{gameId}/Exercise/{exerciseId}")]
    public async Task<IResult> SaveAnswer([FromRoute] Guid userId, [FromRoute] Guid gameId,
        [FromRoute] Guid exerciseId, [FromQuery] double answer, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new SaveExerciseCommand(userId, gameId, exerciseId, answer),
            cancellationToken);

        return result.MatchToHttpResponse(
            resolvedExercise => Results.Ok(_mapper.Map<ResolvedExercise, ResolvedExerciseDto>(resolvedExercise)),
            error => error.Code switch
            {
                GeneralErrorCodes.Validation => Results.BadRequest(error.Description),
                UserErrorCodes.NotFound => Results.NotFound(error.Description),
                GameErrorCodes.NotFound => Results.NotFound(error.Description),
                ExerciseErrorCodes.NotFound => Results.NotFound(error.Description),
                ResolvedGameErrorCodes.NotFound => Results.NotFound(error.Description),
                ResolvedExerciseErrorCodes.ExerciseAlreadyResolved => Results.BadRequest(error.Description),
                _ => throw new InvalidOperationException()
            });
    }
}