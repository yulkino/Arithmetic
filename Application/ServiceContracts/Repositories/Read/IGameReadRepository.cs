using Domain.Entity;
using Domain.Entity.GameEntities;

namespace Application.ServiceContracts.Repositories.Read;

public interface IGameReadRepository : IReadRepository<Game>
{
    ValueTask<Game?> GetGameByIdAsync(Guid gameId, CancellationToken cancellationToken = default);
}