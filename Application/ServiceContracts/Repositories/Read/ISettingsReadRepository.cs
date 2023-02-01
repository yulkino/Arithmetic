using Domain.Entity.SettingsEntities;

namespace Application.ServiceContracts.Repositories.Read;

public interface ISettingsReadRepository : IReadRepository<Settings>
{
    ValueTask<Settings?> GetSettingsAsync(Guid userId, CancellationToken cancellationToken);
}