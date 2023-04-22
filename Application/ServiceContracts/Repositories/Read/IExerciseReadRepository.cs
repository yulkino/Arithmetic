using Domain.Entity.ExerciseEntities;

namespace Application.ServiceContracts.Repositories.Read;

public interface IExerciseReadRepository : IReadRepository<Exercise>
{
    ValueTask<Exercise?> GetExerciseByIdAsync(Guid exerciseId, CancellationToken cancellationToken = default);
}