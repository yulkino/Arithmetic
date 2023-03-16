using Application.PipelineBehavior;
using Application.Services.StatisticServices;
using Domain.Entity.SettingsEntities;
using Domain.StatisticStaff;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class ServiceCollectionExtensions
{
    public static void AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(config => config.RegisterServicesFromAssemblyContaining(typeof(ServiceCollectionExtensions)));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        services.AddValidatorsFromAssemblyContaining(typeof(ServiceCollectionExtensions));
        services.AddScoped<IStatisticCalculator<List<GameStatistic>>, GameStatisticCalculator>();
        services.AddScoped<IStatisticCalculator<Diagram<OperationsStatistic, Operation, TimeSpan>>, OperationsStatisticCalculator>();
        services.AddScoped<IStatisticCalculator<Diagram<ExerciseProgressStatistic, DateTime, TimeSpan>>, ExerciseProgressStatisticCalculator>();
        services.AddScoped<IStatisticCollector, StatisticCollector>();
    }
}