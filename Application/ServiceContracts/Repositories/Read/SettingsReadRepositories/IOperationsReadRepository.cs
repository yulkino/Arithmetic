using Domain.Entity.SettingsEntities;

namespace Application.ServiceContracts.Repositories.Read.SettingsReadRepositories;

public interface IOperationsReadRepository : IReadRepository<Operation>
{
    Task<HashSet<Operation>> GetOperationsAsync(CancellationToken cancellationToken = default);

    Task<HashSet<Operation>> GetOperationsByIdsAsync(List<Guid> id, CancellationToken cancellationToken = default);

    Task<HashSet<Operation>> GetDefaultOperationsAsync(CancellationToken cancellationToken = default);
}