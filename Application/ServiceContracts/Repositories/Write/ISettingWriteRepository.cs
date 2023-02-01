using Domain.Entity.SettingsEntities;

namespace Application.ServiceContracts.Repositories.Write;

public interface ISettingWriteRepository : IRepository<Settings>
{
    ValueTask<Settings?> EditSettingsAsync(Settings settings, CancellationToken cancellationToken);
}