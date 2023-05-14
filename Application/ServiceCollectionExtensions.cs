using Application.Mediators.DifficultyMediator;
using Application.Mediators.GameMediator.Add;
using Application.Mediators.GameMediator.GetExercise;
using Application.Mediators.GameMediator.SaveExercise;
using Application.Mediators.OperationMediator;
using Application.Mediators.ResolvedGameMediator.Get;
using Application.Mediators.SettingsMediator.Edit;
using Application.Mediators.SettingsMediator.Get;
using Application.Mediators.StatisticMediator.Get;
using Application.Mediators.UserMediator.Add;
using Application.Mediators.UserMediator.Get;
using Application.PipelineBehavior;
using Application.Services.SettingsProvider;
using Application.Services.StatisticServices;
using Domain.Entity;
using Domain.Entity.ExerciseEntities;
using Domain.Entity.GameEntities;
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

            config.AddUserBehavior();
            config.AddSettingsBehavior();
            config.AddGameBehavior();
            config.AddResolvedGameBehavior();
            config.AddStatisticBehavior();
        });
        services.AddValidatorsFromAssemblyContaining(typeof(ServiceCollectionExtensions), includeInternalTypes: true);

        services.AddScoped<IStatisticCalculator<List<GameStatistic>>, GameStatisticCalculator>();
        services.AddScoped<IStatisticCalculator<Diagram<OperationsStatistic, Operation, TimeSpan>>,
            OperationsStatisticCalculator>();
        services.AddScoped<IStatisticCalculator<Diagram<ExerciseProgressStatistic, DateTime, TimeSpan>>,
            ExerciseProgressStatisticCalculator>();
        services.AddScoped<IStatisticCollector, StatisticCollector>();

        services.AddScoped<IDefaultSettingsProvider, DefaultSettingsProvider>();
    }

    private static void AddUserBehavior(this MediatRServiceConfiguration config)
    {
        config.AddBehavior<IPipelineBehavior<AddUserCommand, ErrorOr<User>>, ValidationBehavior<AddUserCommand, User>>();
        config.AddBehavior<IPipelineBehavior<GetUserQuery, ErrorOr<User>>, ValidationBehavior<GetUserQuery, User>>();
    }

    private static void AddSettingsBehavior(this MediatRServiceConfiguration config)
    {
        config.AddBehavior<IPipelineBehavior<GetSettingsQuery, ErrorOr<Settings>>,
                ValidationBehavior<GetSettingsQuery, Settings>>();
        config.AddBehavior<IPipelineBehavior<EditSettingsCommand, ErrorOr<Settings>>,
                ValidationBehavior<EditSettingsCommand, Settings>>();
    }

    private static void AddGameBehavior(this MediatRServiceConfiguration config)
    {
        config.AddBehavior<IPipelineBehavior<AddGameCommand, ErrorOr<Game>>, ValidationBehavior<AddGameCommand, Game>>();
        config.AddBehavior<IPipelineBehavior<GetExerciseQuery, ErrorOr<Exercise>>,
                ValidationBehavior<GetExerciseQuery, Exercise>>();
        config.AddBehavior<IPipelineBehavior<SaveExerciseCommand, ErrorOr<ResolvedExercise>>,
                ValidationBehavior<SaveExerciseCommand, ResolvedExercise>>();
    }

    private static void AddResolvedGameBehavior(this MediatRServiceConfiguration config)
    {
        config.AddBehavior<IPipelineBehavior<GetResolvedGameQuery, ErrorOr<ResolvedGame>>,
                ValidationBehavior<GetResolvedGameQuery, ResolvedGame>>();
    }

    private static void AddStatisticBehavior(this MediatRServiceConfiguration config)
    {
        config.AddBehavior<IPipelineBehavior<GetStatisticQuery, ErrorOr<Statistic>>,
                ValidationBehavior<GetStatisticQuery, Statistic>>();
    }
}