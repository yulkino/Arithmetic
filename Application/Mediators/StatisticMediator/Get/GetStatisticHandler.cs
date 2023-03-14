using Application.ServiceContracts.Repositories.Read;
using Application.ServiceContracts.Repositories.Write;
using Domain.Entity;
using Domain.Entity.ExerciseEntities;
using Domain.Entity.GameEntities;
using Domain.Entity.SettingsEntities;
using Domain.StatisticStaff;
using ErrorOr;
using MediatR;

namespace Application.Mediators.StatisticMediator.Get;

public class GetStatisticHandler : IRequestHandler<GetStatisticQuery, ErrorOr<Statistic>>
{
    private readonly IUserReadRepository _userReadRepository;
    private readonly IStatisticReadRepository _statisticReadRepository;
    private readonly IResolvedGameReadRepository _resolvedGameReadRepository;
    private readonly IStatisticWriteRepository _statisticWriteRepository;

    public GetStatisticHandler(IUserReadRepository userReadRepository, IStatisticReadRepository statisticReadRepository,
        IResolvedGameReadRepository resolvedGameReadRepository, IStatisticWriteRepository statisticWriteRepository)
    {
        _userReadRepository = userReadRepository;
        _statisticReadRepository = statisticReadRepository;
        _resolvedGameReadRepository = resolvedGameReadRepository;
        _statisticWriteRepository = statisticWriteRepository;
    }

    public async Task<ErrorOr<Statistic>> Handle(GetStatisticQuery request, CancellationToken cancellationToken)
    {
        var userId = request.UserId;

        var user = await _userReadRepository.GetUserByIdAsync(userId, cancellationToken);
        if (user is null)
            return Error.NotFound("General.NotFound", "User does not exist.");

        var userResolvedGames = await _resolvedGameReadRepository.GetUsersGamesAsync(userId, cancellationToken);
        if (userResolvedGames.Count == 0)
            return Error.Custom(204, "General.Empty", "User has not any games.");

        var userStatistic = await _statisticReadRepository.GetUserStatisticAsync(userId, cancellationToken);

        if (userStatistic is null) 
            return await _statisticWriteRepository.CreateUserStatistic(CalculateStatistic(user, userResolvedGames), cancellationToken);

        if (userStatistic.ResolvedGame.OrderBy(g => g.Id).SequenceEqual(userResolvedGames.OrderBy(g => g.Id)))
            return userStatistic;

        return await _statisticWriteRepository.UpdateUserStatistic(CalculateStatistic(user, userResolvedGames), userStatistic, cancellationToken);
    }

    private Statistic CalculateStatistic(User user, List<ResolvedGame> resolvedGames) 
        => new (user, resolvedGames)
            {
                GameStatisticList = CalculateGameStatistic(resolvedGames),
                OperationsStatisticList = CalculateOperationsStatistic(resolvedGames),
                ExerciseProgressStatisticList = CalculateExerciseProgressStatistic(resolvedGames)
            };

    private List<GameStatistic> CalculateGameStatistic(List<ResolvedGame> resolvedGames)
    {
        var gameStatistics = new List<GameStatistic>();
        resolvedGames.ForEach(resolvedGame =>
        {
            gameStatistics.Add(
                new GameStatistic
                    {
                        GameDate = resolvedGame.Game.Date,
                        ExerciseCount = resolvedGame.ResolvedExercises.Count,
                        CorrectAnswersPercentage =
                            Math.Round(
                                (double)resolvedGame.CorrectAnswerCount * 100 /
                                resolvedGame.ResolvedExercises.Count,
                                2),
                        GameDuration = TimeOnly.FromTimeSpan(resolvedGame.ElapsedTime)
                    });
        });
        return gameStatistics;
    }

    private Diagram<OperationsStatistic, Operation, TimeOnly> CalculateOperationsStatistic(List<ResolvedGame> resolvedGames)
    {
        var operationsStatistic = new Diagram<OperationsStatistic, Operation, TimeOnly>();
        var resolvedExercises = resolvedGames
            .SelectMany(g => g.ResolvedExercises)
            .ToList();
        
        operationsStatistic.AddNode(CalculateOperationStatistic(resolvedExercises, Operation.Addition));
        operationsStatistic.AddNode(CalculateOperationStatistic(resolvedExercises, Operation.Subtraction));
        operationsStatistic.AddNode(CalculateOperationStatistic(resolvedExercises, Operation.Multiplication));
        operationsStatistic.AddNode(CalculateOperationStatistic(resolvedExercises, Operation.Division));
        return operationsStatistic;
    }

    private OperationsStatistic CalculateOperationStatistic(List<ResolvedExercise> resolvedExercises,
        Operation operation)
    {
        var resolvedOperationExercises = resolvedExercises
            .Where(e => e.Exercise.Operation == operation)
            .ToList();

        return new OperationsStatistic(operation, 
            TimeOnly.FromTimeSpan(CalculateAverageTimeSpanFromResolvedExercises(resolvedOperationExercises)));
    }

    private TimeSpan CalculateAverageTimeSpanFromResolvedExercises(List<ResolvedExercise> resolvedExercises)
    {
        var totalTimeElapsed = TimeSpan.Zero;
        resolvedExercises.ForEach(e =>
        {
            totalTimeElapsed += e.ElapsedTime;
        });

        var averageTimeElapsed = TimeSpan.Zero;
        if (totalTimeElapsed != TimeSpan.Zero && resolvedExercises.Count != 0)
            averageTimeElapsed = totalTimeElapsed / resolvedExercises.Count;

        return averageTimeElapsed;
    }

    private Diagram<ExerciseProgressStatistic, DateTime, TimeOnly> CalculateExerciseProgressStatistic(List<ResolvedGame> resolvedGames)
    {
        var exerciseProgressStatistic = new Diagram<ExerciseProgressStatistic, DateTime, TimeOnly>();
        var resolvedGameDate = resolvedGames
            .Select(g => g.Game.Date.Date)
            .Distinct()
            .ToList();

        foreach (var dateTime in resolvedGameDate)
        {
            var resolvedExercises = resolvedGames
                .Where(g => g.Game.Date.Date == dateTime)
                .SelectMany(e => e.ResolvedExercises)
                .ToList();

            var averageTimeElapsed = CalculateAverageTimeSpanFromResolvedExercises(resolvedExercises);

            exerciseProgressStatistic.AddNode(new ExerciseProgressStatistic(dateTime, TimeOnly.FromTimeSpan(averageTimeElapsed)));
        }
        return exerciseProgressStatistic;
    }
}