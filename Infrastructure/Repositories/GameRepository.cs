using Application.ServiceContracts.Repositories.Read;
using Application.ServiceContracts.Repositories.Write;
using Domain.Entity;
using Domain.Entity.GameEntities;
using Domain.Entity.SettingsEntities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class GameRepository : IGameReadRepository, IGameWriteRepository
{
    private readonly ApplicationDbContext _dbContext;

    public GameRepository(ApplicationDbContext dbContext) => _dbContext = dbContext;

    public async ValueTask<Game?> GetGameByIdAsync(Guid gameId, CancellationToken cancellationToken = default) 
        => await _dbContext.Games
            .Include(g => g.Exercises)
            .Include(g => g.User)
            .Include(g => g.Settings).ThenInclude(s => s.Difficulty)
            .Include(g => g.Settings).ThenInclude(s => s.Operations)
            .SingleOrDefaultAsync(g => g.Id == gameId, cancellationToken);

    public async ValueTask<Game> CreateAsync(User user, Settings settings, DateTime creationDate, CancellationToken cancellationToken = default) 
        => (await _dbContext.Games.AddAsync(new Game(user, settings, creationDate), cancellationToken)).Entity;
}