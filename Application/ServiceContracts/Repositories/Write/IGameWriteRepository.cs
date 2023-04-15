using Domain.Entity.GameEntities;
using Domain.Entity.SettingsEntities;

namespace Application.ServiceContracts.Repositories.Write;

public interface IGameWriteRepository : IWriteRepository<Game>
{
    ValueTask<Game> CreateAsync(Guid userId, Settings settings, CancellationToken cancellationToken);
}