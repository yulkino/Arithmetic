using Domain.Entity.ExerciseEntities;

namespace Application.ServiceContracts.Repositories.Write.GameWriteRepositories;

public interface IExerciseWriteRepository : IWriteRepository<Exercise>
{
    ValueTask<Exercise> SaveNextExerciseAsync(Guid userId, Guid gameId, Exercise exercise, CancellationToken cancellationToken);
}