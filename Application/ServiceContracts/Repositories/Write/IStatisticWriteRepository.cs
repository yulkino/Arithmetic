using Domain.Entity;

namespace Application.ServiceContracts.Repositories.Write;

public interface IStatisticWriteRepository : IWriteRepository<Statistic>
{
    ValueTask<Statistic> CreateUserStatistic(Statistic statistic, CancellationToken cancellationToken);
    ValueTask<Statistic> UpdateUserStatistic(Statistic statistic, CancellationToken cancellationToken);
}