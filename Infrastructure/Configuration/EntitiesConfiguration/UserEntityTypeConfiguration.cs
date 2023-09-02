using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration.EntitiesConfiguration;

public class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(user => user.Id);
        builder
            .Property(user => user.Id)
            .ValueGeneratedNever();
        builder
            .HasIndex(user => user.Email)
            .IsUnique();
        builder
            .Property(user => user.PasswordHash)
            .IsRequired();
        builder
            .HasIndex(user => user.IdentityId)
            .IsUnique();
    }
}