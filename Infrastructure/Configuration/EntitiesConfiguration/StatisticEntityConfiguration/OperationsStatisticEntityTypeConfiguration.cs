using Domain.StatisticStaff;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration.EntitiesConfiguration.StatisticEntityConfiguration;

public class OperationsStatisticEntityTypeConfiguration : IEntityTypeConfiguration<OperationsStatistic>
{
    public void Configure(EntityTypeBuilder<OperationsStatistic> builder)
    {
        builder.HasKey(o => o.Id);
        builder
            .Property(o => o.ElementCountStatistic)
            .IsRequired();
        builder
            .Property(o => o.Y)
            .IsRequired();
        builder.HasOne(o => o.X);
    }
}