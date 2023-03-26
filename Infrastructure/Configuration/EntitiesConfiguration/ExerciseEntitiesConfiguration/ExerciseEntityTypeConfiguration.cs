using Domain.Entity.ExerciseEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration.EntitiesConfiguration.ExerciseEntitiesConfiguration;

public class ExerciseEntityTypeConfiguration : IEntityTypeConfiguration<Exercise>
{
    public void Configure(EntityTypeBuilder<Exercise> builder)
    {
        builder.HasKey(exercise => exercise.Id);
        builder
            .Property(exercise => exercise.LeftOperand)
            .IsRequired();
        builder
            .Property(exercise => exercise.RightOperand)
            .IsRequired();
        builder
            .HasOne(exercise => exercise.Operation);
        builder
            .Property(exercise => exercise.Answer)
            .IsRequired();
    }
}