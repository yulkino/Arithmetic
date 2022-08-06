using Domain.Entity.Games;

namespace Application.Mediators.GameMediator.Add;

public record AddGameCommand(Guid UserId) : IOperationRequest<Game>;