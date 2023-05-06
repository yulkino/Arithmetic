using Application.Mediators.UserMediator.Add;
using Application.Mediators.UserMediator.Get;
using Application.PipelineBehavior;
using Application.Services.SettingsProvider;
using Application.Services.StatisticServices;
using Domain.Entity;
using Domain.Entity.SettingsEntities;
using Domain.StatisticStaff;
using ErrorOr;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class ServiceCollectionExtensions
{
    public static void AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssemblyContaining(typeof(ServiceCollectionExtensions));
            config.AddBehavior<IPipelineBehavior<AddUserCommand, ErrorOr<User>>, ValidationBehavior<AddUserCommand, User>>();
            config.AddBehavior<IPipelineBehavior<GetUserQuery, ErrorOr<User>>, ValidationBehavior<GetUserQuery, User>>();
        });
        services.AddValidatorsFromAssemblyContaining(typeof(ServiceCollectionExtensions), includeInternalTypes: true);
        services.AddScoped<IStatisticCalculator<List<GameStatistic>>, GameStatisticCalculator>();
        services.AddScoped<IStatisticCalculator<Diagram<OperationsStatistic, Operation, TimeSpan>>,
                OperationsStatisticCalculator>();
        services.AddScoped<IStatisticCalculator<Diagram<ExerciseProgressStatistic, DateTime, TimeSpan>>,
                ExerciseProgressStatisticCalculator>();
        services.AddScoped<IStatisticCollector, StatisticCollector>();
        services.AddSingleton<IDefaultSettingsProvider, DefaultSettingsProvider>();
    }
}