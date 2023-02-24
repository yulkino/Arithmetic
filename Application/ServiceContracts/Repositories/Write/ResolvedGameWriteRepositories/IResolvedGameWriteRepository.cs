using Domain.Entity.GameEntities;

namespace Application.ServiceContracts.Repositories.Write.ResolvedGameWriteRepositories;

public interface IResolvedGameWriteRepository : IWriteRepository<ResolvedGame>
{
    ValueTask<ResolvedGame?> SaveResolvedGameAsync(Guid gameId, Guid userId, CancellationToken cancellationToken);
}