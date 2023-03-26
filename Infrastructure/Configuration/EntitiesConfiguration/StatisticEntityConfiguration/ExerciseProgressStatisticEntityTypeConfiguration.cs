using Domain.StatisticStaff;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration.EntitiesConfiguration.StatisticEntityConfiguration;

public class ExerciseProgressStatisticEntityTypeConfiguration : IEntityTypeConfiguration<ExerciseProgressStatistic>
{
    public void Configure(EntityTypeBuilder<ExerciseProgressStatistic> builder)
    {
        builder.HasKey(x => x.Id);
        builder
            .Property(e => e.ElementCountStatistic)
            .IsRequired();
        builder
            .Property(e => e.X)
            .IsRequired();
        builder
            .Property(e => e.Y)
            .IsRequired();
    }
}