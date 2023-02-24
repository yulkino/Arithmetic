using Domain.Entity.GameEntities;

namespace Application.ServiceContracts.Repositories.Read.GameReadRepositories;

public interface IGameReadRepository : IReadRepository<Game>
{
    ValueTask<Game?> GetGameByIdAsync(Guid gameId, Guid userId, CancellationToken cancellationToken);
}