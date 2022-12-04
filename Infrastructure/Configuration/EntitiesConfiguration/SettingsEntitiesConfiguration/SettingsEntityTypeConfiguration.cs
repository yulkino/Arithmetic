using Domain.Entity.SettingsEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration.EntitiesConfiguration.SettingsEntitiesConfiguration;

public class SettingsEntityTypeConfiguration : IEntityTypeConfiguration<Settings>
{
    public void Configure(EntityTypeBuilder<Settings> builder)
    {
        builder.HasKey(settings => settings.Id);
        builder
            .Property(settings => settings.ExerciseCount)
            .IsRequired();
        builder.HasMany(settings => settings.Operations);
        builder.HasOne(settings => settings.Difficulty);
    }
}