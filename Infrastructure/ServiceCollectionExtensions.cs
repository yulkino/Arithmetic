using Infrastructure.Configuration;
using Infrastructure.Data;
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
        services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(databaseConnectionOptions.ArithmeticDatabase));
        //TODO repositories DI
    }
}