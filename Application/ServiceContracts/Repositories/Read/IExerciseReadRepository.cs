using Domain.Entity;
using Domain.Entity.ExerciseEntities;
using Domain.Entity.GameEntities;

namespace Application.ServiceContracts.Repositories.Read;

public interface IExerciseReadRepository : IReadRepository<Exercise>
{
    ValueTask<Exercise?> GetExerciseByIdAsync(Game game, Guid exerciseId, CancellationToken cancellationToken = default);
}