using Application.ServiceContracts.Repositories.Read.SettingsReadRepositories;
using Domain.Entity.SettingsEntities;
using Infrastructure.Data;

namespace Infrastructure.Repositories.SettingsRepositories;

public class DifficultiesRepository : IDifficultiesReadRepository
{
    private readonly ApplicationDbContext _dbContext;

    public DifficultiesRepository(ApplicationDbContext dbContext) => _dbContext = dbContext;

    public Task<List<Difficulty>> GetDifficultiesAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public ValueTask<Difficulty?> GetDifficultyByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}