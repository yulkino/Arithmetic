using Application.ServiceContracts.Repositories.Read.SettingsReadRepositories;
using Domain.Entity.SettingsEntities;

namespace Application.Services.SettingsProvider;

public class DefaultSettingsProvider : IDefaultSettingsProvider
{
    private readonly IDifficultiesReadRepository _difficultiesReadRepository;
    private readonly IOperationsReadRepository _operationsReadRepository;

    public DefaultSettingsProvider(IDifficultiesReadRepository difficultiesReadRepository, 
        IOperationsReadRepository operationsReadRepository)
    {
        _difficultiesReadRepository = difficultiesReadRepository;
        _operationsReadRepository = operationsReadRepository;
    }

    public async Task<Settings> GetDefaultSettingsAsync(CancellationToken cancellationToken = default)
    {
        return new Settings(
            await _difficultiesReadRepository.GetDefaultDifficultyAsync(cancellationToken),
            await _operationsReadRepository.GetDefaultOperationsAsync(cancellationToken),
            5);
    }
}