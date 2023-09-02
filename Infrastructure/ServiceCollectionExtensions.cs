using Application.ServiceContracts;
using Application.ServiceContracts.Repositories.Read;
using Application.ServiceContracts.Repositories.Read.SettingsReadRepositories;
using Application.ServiceContracts.Repositories.Write;
using Application.Services.Authentication;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Infrastructure.Configuration;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Infrastructure.Repositories.SettingsRepositories;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class ServiceCollectionExtensions
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        var databaseConnectionOptions = config
            .GetRequiredSection(DatabaseConnectionOptions.SectionName)
            .Get<DatabaseConnectionOptions>()!;
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.AddInterceptors(StaticEntityInterceptor.Instance);
            options.UseSqlServer(databaseConnectionOptions.ArithmeticDatabase);
            options.EnableSensitiveDataLogging();
        });

        services.AddRepositories();
        
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<ITimeProvider, TimeProvider>();

        services.AddFirebaseAuthentication(config);
    }

    private static void AddRepositories(this IServiceCollection services)
    {
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
    }

    private static void AddFirebaseAuthentication(this IServiceCollection services, 
        IConfiguration config)
    {
        FirebaseApp.Create(new AppOptions()
        {
            Credential = GoogleCredential.FromFile("autharithmetic-firebase.json")
        });
        services.AddScoped<IAuthenticationService, AuthenticationService>();
        services.AddHttpClient<IJwtProvider, JwtProvider>(client =>
        {
            client.BaseAddress = new Uri(config["Authentication:TokenUri"] ?? 
                throw new ArgumentException("Authentication TokenUri cannot be null"));
        });
        services
            .AddAuthentication()
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, jwtOptions =>
            {
                jwtOptions.Authority = config["Authentication:ValidIssuer"];
                jwtOptions.Audience = config["Authentication:Audience"];
                jwtOptions.TokenValidationParameters.ValidIssuer = config["Authentication:ValidIssuer"];
            });
    }
}