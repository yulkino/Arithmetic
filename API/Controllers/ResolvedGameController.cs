using API.DTOs.ResolvedGameDtos;
using Application.ClientErrors.ErrorCodes;
using Application.Mediators.ResolvedGameMediator.Get;
using AutoMapper;
using Domain.Entity.GameEntities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace API.Controllers;

[ApiController]
[Authorize]
public sealed class ResolvedGameController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public ResolvedGameController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpPost("User/{userId}/Game/{gameId}/Result")]
    [ProducesResponseType(Status200OK)]
    [ProducesResponseType(Status400BadRequest)]
    [ProducesResponseType(Status404NotFound)]
    public async Task<IResult> GetGameResult([FromRoute] Guid userId, [FromRoute] Guid gameId,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetResolvedGameQuery(userId, gameId), cancellationToken);

        return result.MatchToHttpResponse(
            resolvedGame => Results.Ok(_mapper.Map<ResolvedGame, ResolvedGameDto>(resolvedGame)),
            error => error.Code switch
            {
                GeneralErrorCodes.Validation => Results.BadRequest(error.Description),
                UserErrorCodes.NotFound => Results.NotFound(error.Description),
                GameErrorCodes.NotFound => Results.NotFound(error.Description),
                GameErrorCodes.NotOver => Results.BadRequest(error.Description),
                ResolvedGameErrorCodes.NotFound => Results.NotFound(error.Description),
                _ => throw new InvalidOperationException()
            });
    }
}