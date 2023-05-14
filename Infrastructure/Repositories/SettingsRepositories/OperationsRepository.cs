using Application.ServiceContracts.Repositories.Read.SettingsReadRepositories;
using Domain.Entity.SettingsEntities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.SettingsRepositories;

public class OperationsRepository : IOperationsReadRepository
{
    private readonly ApplicationDbContext _dbContext;

    public OperationsRepository(ApplicationDbContext dbContext) => _dbContext = dbContext;

    public async Task<HashSet<Operation>> GetOperationsAsync(CancellationToken cancellationToken = default)
        => (await _dbContext.Operations.ToListAsync(cancellationToken)).ToHashSet();

    public async Task<HashSet<Operation>> GetOperationsByIdsAsync(List<Guid> ids,
        CancellationToken cancellationToken = default)
        => (await _dbContext.Operations.Where(o => ids.Contains(o.Id)).ToListAsync(cancellationToken)).ToHashSet();

    public async Task<HashSet<Operation>> GetDefaultOperationsAsync(CancellationToken cancellationToken = default)
        => (await _dbContext.Operations
            .Where(o => new List<Guid>()
                        {
                            Operation.Addition.Id, 
                            Operation.Subtraction.Id
                        }
                .Contains(o.Id))
            .ToListAsync(cancellationToken))
            .ToHashSet();
}