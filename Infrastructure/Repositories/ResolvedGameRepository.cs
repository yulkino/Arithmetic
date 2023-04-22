using Application.ServiceContracts.Repositories.Read;
using Application.ServiceContracts.Repositories.Write;
using Domain.Entity.GameEntities;
using Infrastructure.Data;

namespace Infrastructure.Repositories;

public class ResolvedGameRepository : IResolvedGameReadRepository, IResolvedGameWriteRepository
{
    private readonly ApplicationDbContext _dbContext;

    public ResolvedGameRepository(ApplicationDbContext dbContext) => _dbContext = dbContext;

    public ValueTask<ResolvedGame> GetResolvedGameAsync(Guid userId, Guid gameId,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<List<ResolvedGame>> GetUsersGamesAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public ValueTask<ResolvedGame> CreateResolvedGameAsync(Game game, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}