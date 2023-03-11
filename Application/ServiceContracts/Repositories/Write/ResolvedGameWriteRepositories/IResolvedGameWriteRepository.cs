using Domain.Entity.GameEntities;

namespace Application.ServiceContracts.Repositories.Write.ResolvedGameWriteRepositories;

public interface IResolvedGameWriteRepository : IWriteRepository<ResolvedGame>
{
    ValueTask<ResolvedGame> CreateResolvedGameAsync(Game game, CancellationToken cancellationToken);
    ValueTask<ResolvedGame> UpdateResolvedGameAsync(ResolvedGame game, CancellationToken cancellationToken);
}