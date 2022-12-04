using Domain.Entity.SettingsEntities;

namespace Application.Mediators.SettingsMediator.Get;

public record GetSettingsQuery(Guid UserId) : IOperationRequest<Settings>;