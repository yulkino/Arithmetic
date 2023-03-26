using Application.ServiceContracts.Repositories.Read.SettingsReadRepositories;
using Application.ServiceContracts.Repositories.Write;
using Domain.Entity.SettingsEntities;
using Infrastructure.Data;

namespace Infrastructure.Repositories.SettingsRepositories;

public class SettingsRepository : ISettingsReadRepository, ISettingsWriteRepository
{
    private readonly ApplicationDbContext _dbContext;

    public SettingsRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public ValueTask<Settings?> GetSettingsAsync(Guid userId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public ValueTask<Settings> UpdateSettingsAsync(Settings settings, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}