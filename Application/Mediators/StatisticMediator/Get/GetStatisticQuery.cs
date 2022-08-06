using Domain.Entity;

namespace Application.Mediators.StatisticMediator.Get;

public record GetStatisticQuery(Guid UserId) : IOperationRequest<Statistic>;