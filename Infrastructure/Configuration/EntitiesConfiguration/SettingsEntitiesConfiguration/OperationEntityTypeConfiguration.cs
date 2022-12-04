using Domain.Entity.SettingsEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using static Domain.Entity.SettingsEntities.Operation;

namespace Infrastructure.Configuration.EntitiesConfiguration.SettingsEntitiesConfiguration;

public class OperationEntityTypeConfiguration : IEntityTypeConfiguration<Operation>
{
    public void Configure(EntityTypeBuilder<Operation> builder)
    {
        builder.HasKey(operation => operation.Id);
        builder.HasData(Addition, Division, Multiplication, Subtraction);
    }
}