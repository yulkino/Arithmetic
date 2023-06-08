using Domain.Entity;
using Domain.Entity.ExerciseEntities;
using Domain.Entity.GameEntities;
using Domain.Entity.SettingsEntities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) => Database.EnsureCreated();

    public DbSet<User> Users { get; set; }
    public DbSet<Difficulty> Difficulties { get; set; }
    public DbSet<Operation> Operations { get; set; }
    public DbSet<Settings> Settings { get; set; }
    public DbSet<Exercise> Exercises { get; set; }
    public DbSet<ResolvedExercise> ResolvedExercises { get; set; }
    public DbSet<Game> Games { get; set; }
    public DbSet<ResolvedGame> ResolvedGames { get; set; }
    public DbSet<Statistic> Statistics { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}