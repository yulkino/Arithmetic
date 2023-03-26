using Domain.Entity.GameEntities;

namespace Application.ServiceContracts.Repositories.Write;

public interface IGameWriteRepository : IWriteRepository<Game>
{
    ValueTask<Game> CreateAsync(Guid userId, CancellationToken cancellationToken);
}