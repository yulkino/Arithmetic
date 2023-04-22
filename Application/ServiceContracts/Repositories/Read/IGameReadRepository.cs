using Domain.Entity.GameEntities;

namespace Application.ServiceContracts.Repositories.Read;

public interface IGameReadRepository : IReadRepository<Game>
{
    ValueTask<Game?> GetGameByIdAsync(Guid gameId, Guid userId, CancellationToken cancellationToken = default);
}