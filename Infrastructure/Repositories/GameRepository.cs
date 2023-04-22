using Application.ServiceContracts.Repositories.Read;
using Application.ServiceContracts.Repositories.Write;
using Domain.Entity.GameEntities;
using Domain.Entity.SettingsEntities;
using Infrastructure.Data;

namespace Infrastructure.Repositories;

public class GameRepository : IGameReadRepository, IGameWriteRepository
{
    private readonly ApplicationDbContext _dbContext;

    public GameRepository(ApplicationDbContext dbContext) => _dbContext = dbContext;

    public ValueTask<Game?> GetGameByIdAsync(Guid gameId, Guid userId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public ValueTask<Game> CreateAsync(Guid userId, Settings settings, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}