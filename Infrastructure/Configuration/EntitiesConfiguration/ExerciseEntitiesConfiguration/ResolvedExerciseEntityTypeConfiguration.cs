using Domain.Entity.ExerciseEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration.EntitiesConfiguration.ExerciseEntitiesConfiguration;

public class ResolvedExerciseEntityTypeConfiguration : IEntityTypeConfiguration<ResolvedExercise>
{
    public void Configure(EntityTypeBuilder<ResolvedExercise> builder)
    {

        builder.HasKey(resolvedExercise => resolvedExercise.Id);
        builder
            .Property(resolvedExercise => resolvedExercise.UserAnswer)
            .IsRequired();
        builder
            .Property(resolvedExercise => resolvedExercise.IsCorrect)
            .IsRequired();
        builder
            .Property(resolvedExercise => resolvedExercise.ElapsedTime)
            .IsRequired();
        builder.HasOne(resolvedExercise => resolvedExercise.Exercise);

    }
}