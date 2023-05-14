using Domain.Entity;
using Domain.Entity.GameEntities;

namespace Application.ServiceContracts.Repositories.Read;

public interface IResolvedGameReadRepository : IReadRepository<ResolvedGame>
{
    ValueTask<ResolvedGame?> GetResolvedGameAsync(Game game, CancellationToken cancellationToken = default);

    Task<List<ResolvedGame>> GetUsersGamesAsync(User user, CancellationToken cancellationToken = default);
}