using Domain.Entity.SettingsEntities;

namespace Application.Mediators.OperationMediator.Get;

public record GetOperationsQuery : IOperationRequest<HashSet<Operation>>;