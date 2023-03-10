using Domain.Entity.SettingsEntities;

namespace Application.ServiceContracts.Repositories.Write;

public interface ISettingWriteRepository : IWriteRepository<Settings>
{
    ValueTask<Settings> UpdateSettingsAsync(Settings settings, CancellationToken cancellationToken);
}