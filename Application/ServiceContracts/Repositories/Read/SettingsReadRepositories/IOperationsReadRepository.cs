using Domain.Entity.SettingsEntities;

namespace Application.ServiceContracts.Repositories.Read.SettingsReadRepositories;

public interface IOperationsReadRepository : IReadRepository<Operation>
{
    Task<List<Operation>> GetOperationsAsync(CancellationToken cancellationToken = default);

    Task<HashSet<Operation>> GetOperationsByIdsAsync(List<Guid> id, CancellationToken cancellationToken = default);
}