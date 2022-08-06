using Domain.Entity;

namespace Application.Mediators.SettingsMediator.Get;

public record GetSettingsQuery(Guid UserId) : IOperationRequest<Settings>;