using Application.ServiceContracts;
using Application.ServiceContracts.Repositories.Read;
using Application.ServiceContracts.Repositories.Read.SettingsReadRepositories;
using Application.ServiceContracts.Repositories.Write;
using Application.Services;
using Infrastructure.Configuration;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Infrastructure.Repositories.SettingsRepositories;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class ServiceCollectionExtensions
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var databaseConnectionOptions = configuration
            .GetRequiredSection(DatabaseConnectionOptions.SectionName)
            .Get<DatabaseConnectionOptions>()!;
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.AddInterceptors(new StaticEntityInterceptor());
            options.UseSqlServer(databaseConnectionOptions.ArithmeticDatabase);
            options.EnableSensitiveDataLogging();
        });

        services.AddScoped<IUserReadRepository, UserRepository>();
        services.AddScoped<IUserWriteRepository, UserRepository>();

        services.AddScoped<IStatisticReadRepository, StatisticRepository>();
        services.AddScoped<IStatisticWriteRepository, StatisticRepository>();

        services.AddScoped<IGameReadRepository, GameRepository>();
        services.AddScoped<IGameWriteRepository, GameRepository>();

        services.AddScoped<IResolvedGameReadRepository, ResolvedGameRepository>();
        services.AddScoped<IResolvedGameWriteRepository, ResolvedGameRepository>();

        services.AddScoped<IExerciseReadRepository, ExerciseRepository>();

        services.AddScoped<ISettingsReadRepository, SettingsRepository>();

        services.AddScoped<IOperationsReadRepository, OperationsRepository>();
        services.AddScoped<IDifficultiesReadRepository, DifficultiesRepository>();

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddTransient<ITimeProvider, TimeProvider>();
    }
}