using Domain.Entity.SettingsEntities;

namespace Application.ServiceContracts.Repositories.Read.SettingsReadRepositories;

public interface ISettingsReadRepository : IReadRepository<Settings>
{
    ValueTask<Settings?> GetSettingsAsync(Guid userId, CancellationToken cancellationToken = default);
}