using Domain.Entity.GameEntities;

namespace Application.ServiceContracts.Repositories.Read;

public interface IResolvedGameRepository : IReadRepository<ResolvedGame>
{
    ValueTask<ResolvedGame?> GetResolvedGameAsync(Guid userId, Guid gameId, CancellationToken cancellationToken);
}