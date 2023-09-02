using Application.ServiceContracts.Repositories.Read.SettingsReadRepositories;
using Domain.Entity.SettingsEntities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.SettingsRepositories;

internal class DifficultiesRepository : IDifficultiesReadRepository
{
    private readonly ApplicationDbContext _dbContext;

    public DifficultiesRepository(ApplicationDbContext dbContext) => _dbContext = dbContext;

    public async Task<HashSet<Difficulty>> GetDifficultiesAsync(CancellationToken cancellationToken = default)
        => (await _dbContext.Difficulties.ToListAsync(cancellationToken)).ToHashSet();

    public async ValueTask<Difficulty?> GetDifficultyByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => await _dbContext.Difficulties.SingleOrDefaultAsync(d => d.Id == id, cancellationToken);

    public async ValueTask<Difficulty> GetDefaultDifficultyAsync(CancellationToken cancellationToken = default)
        => await _dbContext.Difficulties.SingleAsync(d => d.Id == Difficulty.Easy.Id, cancellationToken);
}