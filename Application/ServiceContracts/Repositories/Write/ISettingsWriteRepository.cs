using Domain.Entity.SettingsEntities;

namespace Application.ServiceContracts.Repositories.Write;

public interface ISettingsWriteRepository : IWriteRepository<Settings>
{
    ValueTask<Settings> UpdateSettingsAsync(Settings settings, CancellationToken cancellationToken);
}