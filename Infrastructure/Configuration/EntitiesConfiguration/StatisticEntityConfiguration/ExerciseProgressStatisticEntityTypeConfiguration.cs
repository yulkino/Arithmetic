using Domain.StatisticStaff;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration.EntitiesConfiguration.StatisticEntityConfiguration;

public class ExerciseProgressStatisticEntityTypeConfiguration : IEntityTypeConfiguration<ExerciseProgressStatistic>
{
    public void Configure(EntityTypeBuilder<ExerciseProgressStatistic> builder)
    {
        builder.HasKey(exerciseProgressStatistic => exerciseProgressStatistic.Id); 
        builder
            .Property(exerciseProgressStatistic => exerciseProgressStatistic.Id)
            .ValueGeneratedNever();
        builder
            .Property(exerciseProgressStatistic => exerciseProgressStatistic.ElementCountStatistic)
            .IsRequired();
        builder
            .Property(exerciseProgressStatistic => exerciseProgressStatistic.X)
            .IsRequired();
        builder
            .Property(exerciseProgressStatistic => exerciseProgressStatistic.Y)
            .IsRequired()
            .HasConversion(
                t => t.TotalMilliseconds, 
                t => TimeSpan.FromMilliseconds(t));
    }
}