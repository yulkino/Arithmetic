using Application.ServiceContracts.Repositories.Read.SettingsReadRepositories;
using Domain.Entity.GameEntities;
using Domain.Entity.SettingsEntities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.SettingsRepositories;

internal class SettingsRepository : ISettingsReadRepository
{
    private readonly ApplicationDbContext _dbContext;

    public SettingsRepository(ApplicationDbContext dbContext) => _dbContext = dbContext;

    public async ValueTask<Settings?> GetSettingsAsync(Game game, CancellationToken cancellationToken = default) 
        => (await _dbContext.Games
            .Include(g => g.Settings).ThenInclude(s => s.Operations)
            .Include(g => g.Settings).ThenInclude(s => s.Difficulty)
            .SingleOrDefaultAsync(g => g.Equals(game), cancellationToken))?.Settings;
}