using Application.ServiceContracts.Repositories.Read.SettingsReadRepositories;
using Domain.Entity.SettingsEntities;
using Infrastructure.Data;

namespace Infrastructure.Repositories.SettingsRepositories;

public class OperationsRepository : IOperationsReadRepository
{
    private readonly ApplicationDbContext _dbContext;

    public OperationsRepository(ApplicationDbContext dbContext) => _dbContext = dbContext;

    public Task<List<Operation>> GetOperationsAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<HashSet<Operation>> GetOperationsByIdsAsync(List<Guid> id,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}