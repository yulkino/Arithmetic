using Domain.Entity.GameEntities;

namespace Application.ServiceContracts.Repositories.Read;

public interface IResolvedGameReadRepository : IReadRepository<ResolvedGame>
{
    ValueTask<ResolvedGame> GetResolvedGameAsync(Guid userId, Guid gameId,
        CancellationToken cancellationToken = default);

    Task<List<ResolvedGame>> GetUsersGamesAsync(Guid userId, CancellationToken cancellationToken = default);
}