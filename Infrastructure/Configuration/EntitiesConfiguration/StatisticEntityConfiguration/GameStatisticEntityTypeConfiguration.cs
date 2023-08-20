using Domain.StatisticStaff;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration.EntitiesConfiguration.StatisticEntityConfiguration;

public class GameStatisticEntityTypeConfiguration : IEntityTypeConfiguration<GameStatistic>
{
    public void Configure(EntityTypeBuilder<GameStatistic> builder)
    {
        builder.HasKey(gameStatistic => gameStatistic.Id);
        builder
            .Property(gameStatistic => gameStatistic.Id)
            .ValueGeneratedNever();
        builder
            .Property(gameStatistic => gameStatistic.ExerciseCount)
            .IsRequired();
        builder
            .Property(gameStatistic => gameStatistic.CorrectAnswersPercentage)
            .IsRequired();
        builder
            .Property(gameStatistic => gameStatistic.GameDuration)
            .IsRequired()
            .HasConversion(
                t => t.TotalMilliseconds, 
                t => TimeSpan.FromMilliseconds(t));
        builder
            .Property(gameStatistic => gameStatistic.GameDate)  
            .IsRequired();
    }
}