using Application.ServiceContracts.Repositories.Read;
using Application.ServiceContracts.Repositories.Write;
using Domain.Entity;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

internal class StatisticRepository : IStatisticReadRepository, IStatisticWriteRepository
{
    private readonly ApplicationDbContext _dbContext;

    public StatisticRepository(ApplicationDbContext dbContext) => _dbContext = dbContext;

    public async ValueTask<Statistic?> GetUserStatisticAsync(User user, CancellationToken cancellationToken = default) => 
        await _dbContext.Statistic
            .Include(s => s.OperationsStatistic)
            .Include(s => s.ExerciseProgressStatistic)
            .Include(s => s.GameStatistic)
            .Include(s => s.ResolvedGame)
                .ThenInclude(r => r.ResolvedExercises)
                .ThenInclude(e => e.Exercise)
                .ThenInclude(e => e.Operation)
            .Include(s => s.ResolvedGame)
                .ThenInclude(r => r.Game)
                .ThenInclude(g => g.Exercises)
            .Include(s => s.ResolvedGame)
                .ThenInclude(r => r.Game)
                .ThenInclude(g => g.Settings)
                .ThenInclude(s => s.Operations)
            .Include(s => s.User)
            .SingleOrDefaultAsync(s => s.User.Equals(user), cancellationToken);

    public ValueTask<Statistic> CreateUserStatistic(Statistic statistic, CancellationToken cancellationToken = default) => 
        ValueTask.FromResult(_dbContext.Add(statistic).Entity);
}