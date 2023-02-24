using Domain.Entity.ExerciseEntities;

namespace Application.ServiceContracts.Repositories.Read.GameReadRepositories;

public interface IExerciseReadRepository : IReadRepository<Exercise>
{
    ValueTask<Exercise> GetExerciseAsync(Guid userId, Guid gameId, CancellationToken cancellationToken);
    ValueTask<Exercise?> GetExerciseByIdAsync(Guid exerciseId, CancellationToken cancellationToken);
}