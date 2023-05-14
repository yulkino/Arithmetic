using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration.EntitiesConfiguration.StatisticEntityConfiguration;

public class StatisticEntityTypeConfiguration : IEntityTypeConfiguration<Statistic>
{
    public void Configure(EntityTypeBuilder<Statistic> builder)
    {
        builder.HasKey(statistic => statistic.Id);
        builder
            .Property(statistic => statistic.Id)
            .ValueGeneratedNever();
        builder.HasOne(statistic => statistic.User);
        builder.HasMany(statistic => statistic.ResolvedGame);
        builder.HasMany(statistic => statistic.GameStatisticList);
        builder.HasMany(statistic => statistic.OperationsStatisticList);
        builder.HasMany(statistic => statistic.ExerciseProgressStatisticList);
    }
}