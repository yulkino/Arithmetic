using API.DTOs.StatisticDtos;
using Application.ClientErrors.ErrorCodes;
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
    public async Task<IResult> GetUserStatistic([FromRoute] Guid userId,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetStatisticQuery(userId), cancellationToken);

        return result.MatchToHttpResponse(
            statistic => Results.Ok(_mapper.Map<Statistic, StatisticDto>(statistic)),
            error => error.Code switch
            {
                UserErrorCodes.NotFound => Results.NotFound(error.Description),
                _ => throw new InvalidOperationException()
            });
    }
}