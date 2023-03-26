using Domain.StatisticStaff;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration.EntitiesConfiguration.StatisticEntityConfiguration;

public class GameStatisticEntityTypeConfiguration : IEntityTypeConfiguration<GameStatistic>
{
    public void Configure(EntityTypeBuilder<GameStatistic> builder)
    {
        builder.HasKey(g => g.Id);
        builder
            .Property(g => g.ExerciseCount)
            .IsRequired();
        builder
            .Property(g => g.CorrectAnswersPercentage)
            .IsRequired();
        builder
            .Property(g => g.GameDuration)
            .IsRequired();
        builder
            .Property(g => g.GameDate)
            .IsRequired();
    }
}