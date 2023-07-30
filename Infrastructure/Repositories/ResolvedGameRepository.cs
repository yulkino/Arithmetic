using Application.ServiceContracts.Repositories.Read;
using Application.ServiceContracts.Repositories.Write;
using Domain.Entity;
using Domain.Entity.GameEntities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ResolvedGameRepository : IResolvedGameReadRepository, IResolvedGameWriteRepository
{
    private readonly ApplicationDbContext _dbContext;

    public ResolvedGameRepository(ApplicationDbContext dbContext) => _dbContext = dbContext;

    public async ValueTask<ResolvedGame?> GetResolvedGameAsync(Game game, CancellationToken cancellationToken = default)
        => await _dbContext.ResolvedGames
            .Include(g => g.Game)
            .Include(r => r.ResolvedExercises)
            .SingleOrDefaultAsync(r => r.Game.Equals(game), cancellationToken);

    public async Task<List<ResolvedGame>> GetUsersGamesAsync(User user, CancellationToken cancellationToken = default) 
        => await _dbContext.ResolvedGames
            .Where(r => r.Game.User.Equals(user))
            .Include(g => g.Game)
                .ThenInclude(g => g.Settings)
                .ThenInclude(s => s.Difficulty)
            .Include(g => g.Game)
                .ThenInclude(g => g.Settings)
                .ThenInclude(s => s.Operations)
            .Include(g => g.Game)
                .ThenInclude(g => g.Exercises).
                ThenInclude(e => e.Operation)
            .Include(r => r.ResolvedExercises)
                .ThenInclude(r => r.Exercise)
                .ThenInclude(e => e.Operation)
            .ToListAsync(cancellationToken);

    public async ValueTask<ResolvedGame> CreateResolvedGameAsync(Game game, CancellationToken cancellationToken = default)
        => (await _dbContext.ResolvedGames.AddAsync(new ResolvedGame(game), cancellationToken)).Entity;
}