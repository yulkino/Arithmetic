using Domain.StatisticStaff;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration.EntitiesConfiguration.StatisticEntityConfiguration;

public class OperationsStatisticEntityTypeConfiguration : IEntityTypeConfiguration<OperationsStatistic>
{
    public void Configure(EntityTypeBuilder<OperationsStatistic> builder)
    {
        builder.HasKey(operationsStatistic => operationsStatistic.Id);
        builder
            .Property(operationsStatistic => operationsStatistic.Id)
            .ValueGeneratedNever();
        builder
            .Property(operationsStatistic => operationsStatistic.ElementCountStatistic)
            .IsRequired();
        builder
            .Property(operationsStatistic => operationsStatistic.Y)
            .IsRequired()
            .HasConversion(
                t => t.TotalMilliseconds, 
                t => TimeSpan.FromMilliseconds(t));
        builder.HasOne(operationsStatistic => operationsStatistic.X);
    }
}