using Domain.Entity.GameEntities;

namespace Application.Mediators.GameMediator.Add;

public record AddGameCommand(Guid UserId) : IOperationRequest<Game>;