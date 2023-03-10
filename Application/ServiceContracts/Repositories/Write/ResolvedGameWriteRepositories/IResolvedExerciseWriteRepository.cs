using Domain.Entity.ExerciseEntities;

namespace Application.ServiceContracts.Repositories.Write.ResolvedGameWriteRepositories;

public interface IResolvedExerciseWriteRepository : IWriteRepository<ResolvedExercise>
{
    ValueTask<ResolvedExercise> SaveResolvedExerciseAsync(ResolvedExercise resolvedExercise, CancellationToken cancellationToken);
}