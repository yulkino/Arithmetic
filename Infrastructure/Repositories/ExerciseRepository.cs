using Application.ServiceContracts.Repositories.Read;
using Application.ServiceContracts.Repositories.Write;
using Domain.Entity.ExerciseEntities;
using Infrastructure.Data;

namespace Infrastructure.Repositories;

public class ExerciseRepository : IExerciseReadRepository
{
    private readonly ApplicationDbContext _dbContext;

    public ExerciseRepository(ApplicationDbContext dbContext) => _dbContext = dbContext;

    public ValueTask<Exercise?> GetExerciseByIdAsync(Guid exerciseId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}