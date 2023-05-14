using Domain.Entity.GameEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration.EntitiesConfiguration.GameEntitiesConfiguration;

public class ResolvedGameEntityTypeConfiguration : IEntityTypeConfiguration<ResolvedGame>
{
    public void Configure(EntityTypeBuilder<ResolvedGame> builder)
    {
        builder.HasKey(resolvedGame => resolvedGame.Id);
        builder
            .Property(resolvedGame => resolvedGame.Id)
            .ValueGeneratedNever();
        builder
            .Property(resolvedGame => resolvedGame.CorrectAnswerCount)
            .IsRequired();
        builder
            .Property(resolvedGame => resolvedGame.ElapsedTime)
            .IsRequired();
        builder.HasOne(resolvedGame => resolvedGame.Game);
        builder.HasMany(resolvedGame => resolvedGame.ResolvedExercises);
    }
}