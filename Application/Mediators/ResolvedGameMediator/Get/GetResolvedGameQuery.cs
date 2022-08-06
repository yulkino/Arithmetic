using Domain.Entity.Games;

namespace Application.Mediators.ResolvedGameMediator.Get;

public record GetResolvedGameQuery(Guid UserId, Guid GameId) : IOperationRequest<ResolvedGame>;