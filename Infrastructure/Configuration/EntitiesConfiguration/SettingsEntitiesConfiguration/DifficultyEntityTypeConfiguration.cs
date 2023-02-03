using Domain.Entity.SettingsEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using static Domain.Entity.SettingsEntities.Difficulty;

namespace Infrastructure.Configuration.EntitiesConfiguration.SettingsEntitiesConfiguration;

public class DifficultyEntityTypeConfiguration : IEntityTypeConfiguration<Difficulty>
{
    public void Configure(EntityTypeBuilder<Difficulty> builder)
    {
        builder.HasKey(difficulty => difficulty.Id);
        builder
            .Property(difficulty => difficulty.Name)
            .IsRequired();
        builder
            .Property(difficulty => difficulty.MaxDigitCount)
            .IsRequired();
        builder.HasData(Easy, Medium, Hard);
    }
}