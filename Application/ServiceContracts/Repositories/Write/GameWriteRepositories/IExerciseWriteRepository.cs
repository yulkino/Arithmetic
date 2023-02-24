using Domain.Entity.ExerciseEntities;

namespace Application.ServiceContracts.Repositories.Write.GameWriteRepositories;

public interface IExerciseWriteRepository : IWriteRepository<Exercise>
{
    ValueTask<Exercise?> SaveExerciseAsync(Guid userId, Guid gameId, double answer, CancellationToken cancellationToken);
}