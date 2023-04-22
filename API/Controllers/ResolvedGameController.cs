using API.DTOs.ResolvedGameDtos;
using Application.Mediators.ResolvedGameMediator.Get;
using AutoMapper;
using Domain.Entity.GameEntities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
public sealed class ResolvedGameController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public ResolvedGameController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpGet("User/{userId}/Game/{gameId}/Result")]
    public async Task<ActionResult<ResolvedGameDto>> GetGameResult([FromRoute] Guid userId, [FromRoute] Guid gameId,
        CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new GetResolvedGameQuery(userId, gameId), cancellationToken);
        //TODO error catch
        var result = _mapper.Map<ResolvedGame, ResolvedGameDto>(response.Value);
        return result;
    }
}