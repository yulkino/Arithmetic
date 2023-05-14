using Domain.Entity.SettingsEntities;

namespace Application.Services.SettingsProvider;

public interface IDefaultSettingsProvider
{
    Task<Settings> GetDefaultSettingsAsync(CancellationToken cancellationToken = default);
}