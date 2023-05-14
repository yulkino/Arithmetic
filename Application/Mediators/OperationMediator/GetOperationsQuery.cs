using Domain.Entity.SettingsEntities;

namespace Application.Mediators.OperationMediator;

public record GetOperationsQuery : IOperationRequest<HashSet<Operation>>;