using Domain.Entity.SettingsEntities;

namespace Application.ServiceContracts.Repositories.Read.SettingsReadRepositories;

public interface IDifficultiesReadRepository : IReadRepository<Difficulty>
{
    Task<HashSet<Difficulty>> GetDifficultiesAsync(CancellationToken cancellationToken = default);
    ValueTask<Difficulty?> GetDifficultyByIdAsync(Guid id, CancellationToken cancellationToken = default);
    ValueTask<Difficulty> GetDefaultDifficultyAsync(CancellationToken cancellationToken = default);
}