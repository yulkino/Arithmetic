using Application.ServiceContracts.Repositories.Read;
using Domain.Entity.ExerciseEntities;
using Domain.Entity.GameEntities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ExerciseRepository : IExerciseReadRepository
{
    private readonly ApplicationDbContext _dbContext;

    public ExerciseRepository(ApplicationDbContext dbContext) => _dbContext = dbContext;

    public async ValueTask<Exercise?> GetExerciseByIdAsync(Game game, Guid exerciseId,
        CancellationToken cancellationToken = default)
        => (await _dbContext.Games
                .Include(g => g.Settings)
                .Include(g => g.Exercises)
                .SingleOrDefaultAsync(g => g.Equals(game), cancellationToken))?.Exercises
            .SingleOrDefault(e => e.Id == exerciseId);
}