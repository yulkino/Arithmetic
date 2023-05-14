using Domain.Entity.GameEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration.EntitiesConfiguration.GameEntitiesConfiguration;

public class GameEntityTypeConfiguration : IEntityTypeConfiguration<Game>
{
    public void Configure(EntityTypeBuilder<Game> builder)
    {
        builder.HasKey(game => game.Id);
        builder
            .Property(game => game.Id)
            .ValueGeneratedNever();
        builder
            .Property(game => game.Date)
            .IsRequired();
        builder.HasOne(game => game.Settings);
        builder.HasOne(game => game.User);
        builder.HasMany(game => game.Exercises);
    }
}