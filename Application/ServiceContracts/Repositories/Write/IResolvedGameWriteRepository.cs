using Domain.Entity.GameEntities;

namespace Application.ServiceContracts.Repositories.Write;

public interface IResolvedGameWriteRepository : IWriteRepository<ResolvedGame>
{
    ValueTask<ResolvedGame> CreateResolvedGameAsync(Game game, CancellationToken cancellationToken = default);
}