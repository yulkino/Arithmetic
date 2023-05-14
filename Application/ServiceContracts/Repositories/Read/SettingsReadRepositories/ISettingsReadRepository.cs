using Domain.Entity.GameEntities;
using Domain.Entity.SettingsEntities;

namespace Application.ServiceContracts.Repositories.Read.SettingsReadRepositories;

public interface ISettingsReadRepository : IReadRepository<Settings>
{
    ValueTask<Settings?> GetSettingsAsync(Game game, CancellationToken cancellationToken = default);
}