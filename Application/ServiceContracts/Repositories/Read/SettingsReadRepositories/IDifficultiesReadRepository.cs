using Domain.Entity.SettingsEntities;

namespace Application.ServiceContracts.Repositories.Read.SettingsReadRepositories;

public interface IDifficultiesReadRepository : IReadRepository<Difficulty>
{
    Task<List<Difficulty>> GetDifficultyListAsync(CancellationToken cancellationToken);
}