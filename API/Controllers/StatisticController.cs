using API.DTOs.StatisticDtos;
using Application.Mediators.StatisticMediator.Get;
using AutoMapper;
using Domain.Entity;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
public sealed class StatisticController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public StatisticController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpPost("User/{userId}/Statistic")]
    public async Task<ActionResult<StatisticDto>> GetStatisticForGame([FromRoute] Guid userId,
        CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new GetStatisticQuery(userId), cancellationToken);
        //TODO error catch
        var result = _mapper.Map<Statistic, StatisticDto>(response.Value);
        return result;
    }
}