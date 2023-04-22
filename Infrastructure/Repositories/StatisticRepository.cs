using Application.ServiceContracts.Repositories.Read;
using Application.ServiceContracts.Repositories.Write;
using Domain.Entity;
using Infrastructure.Data;

namespace Infrastructure.Repositories;

public class StatisticRepository : IStatisticReadRepository, IStatisticWriteRepository
{
    private readonly ApplicationDbContext _dbContext;

    public StatisticRepository(ApplicationDbContext dbContext) => _dbContext = dbContext;

    public ValueTask<Statistic?> GetUserStatisticAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public ValueTask<Statistic> CreateUserStatistic(Statistic statistic, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public ValueTask<Statistic> UpdateUserStatistic(Statistic statistic, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}