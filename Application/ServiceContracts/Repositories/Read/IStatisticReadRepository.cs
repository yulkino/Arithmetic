using Domain.Entity;

namespace Application.ServiceContracts.Repositories.Read;

public interface IStatisticReadRepository : IReadRepository<Statistic>
{
    ValueTask<Statistic?> GetUserStatisticAsync(Guid userId, CancellationToken cancellationToken);
}