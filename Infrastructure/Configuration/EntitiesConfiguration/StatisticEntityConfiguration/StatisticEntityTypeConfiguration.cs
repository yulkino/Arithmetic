using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration.EntitiesConfiguration.StatisticEntityConfiguration;

public class StatisticEntityTypeConfiguration : IEntityTypeConfiguration<Statistic>
{
    public void Configure(EntityTypeBuilder<Statistic> builder)
    {
        builder.HasKey(s => s.Id);
        builder.HasOne(s => s.User);
        builder.HasMany(s => s.ResolvedGame);
        builder.HasMany(s => s.GameStatisticList);
        builder.HasMany(s => s.OperationsStatisticList);
        builder.HasMany(s => s.ExerciseProgressStatisticList);
    }
}