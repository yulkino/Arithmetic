using Domain.Entity.SettingsEntities;

namespace Application.ServiceContracts.Repositories.Read.SettingsReadRepositories;

public interface IDifficultiesReadRepository : IReadRepository<Difficulty>
{
    Task<List<Difficulty>> GetDifficultiesAsync(CancellationToken cancellationToken);
    ValueTask<Difficulty?> GetDifficultyByIdAsync(Guid id, CancellationToken cancellationToken);
}