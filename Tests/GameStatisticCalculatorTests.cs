using Application.Services.StatisticServices;
using Domain.Entity;
using Domain.Entity.ExerciseEntities;
using Domain.Entity.GameEntities;
using Domain.Entity.SettingsEntities;
using Domain.StatisticStaff;
using FluentAssertions;

namespace Tests;

public class GameStatisticCalculatorTests
{
    private GameStatisticCalculator _calculator;
    private List<ResolvedGame> _resolvedGames;

    [SetUp]
    public void Setup()
    {
        _calculator = new GameStatisticCalculator();
        var user = new User("TestLogin", "TestPassword");
        var settings = new Settings(
            Difficulty.Medium,
            new HashSet<Operation>()
            {
                Operation.Addition, Operation.Division, Operation.Multiplication, Operation.Subtraction
            },
            4);
        _resolvedGames = new List<ResolvedGame>()
        {
            new(new Game(user, settings)), 
            new(new Game(user, settings)), 
            new(new Game(user, settings))
        };
        _resolvedGames[0].ResolvedExercises
            .AddRange(new List<ResolvedExercise>()
            {
                new(45, TimeSpan.FromSeconds(2), new Exercise(10, 35, Operation.Addition)), //+
                new(5, TimeSpan.FromSeconds(6), new Exercise(35, 35, Operation.Division)), //-
                new(350, TimeSpan.FromSeconds(8), new Exercise(10, 35, Operation.Multiplication)), //+
                new(-25, TimeSpan.FromSeconds(4), new Exercise(10, 35, Operation.Subtraction)) //+
            });

        _resolvedGames[1].ResolvedExercises
            .AddRange(new List<ResolvedExercise>()
            {
                new(200, TimeSpan.FromSeconds(2), new Exercise(100, 100, Operation.Addition)), //+
                new(30, TimeSpan.FromSeconds(6), new Exercise(5, 35, Operation.Subtraction)), //-
                new(350, TimeSpan.FromSeconds(8), new Exercise(10, 35, Operation.Multiplication)), //+
                new(10, TimeSpan.FromSeconds(4), new Exercise(100, 50, Operation.Subtraction)) //-
            });

        _resolvedGames[2].ResolvedExercises
            .AddRange(new List<ResolvedExercise>()
            {
                new(300, TimeSpan.FromSeconds(2), new Exercise(10, 35, Operation.Multiplication)), //-
                new(5, TimeSpan.FromSeconds(6), new Exercise(444, 4, Operation.Division)), //-
                new(35, TimeSpan.FromSeconds(8), new Exercise(10, 35, Operation.Multiplication)), //-
                new(20, TimeSpan.FromSeconds(4), new Exercise(200, 10, Operation.Division)) //+
            });

        _resolvedGames.ForEach(r => r.ProcessGameResult());
    }

    [Test]
    public void Calculate_Should_ReturnGameStatistic_When_ReceivesResolvedGames()
    {
        var exerciseCount = 4;
        var firstGameCorrectAnswersPercentage = 75d;
        var secondGameCorrectAnswersPercentage = 50d;
        var thirdGameCorrectAnswersPercentage = 25d;
        var gameDuration = TimeSpan.FromSeconds(20);

        var statistic = _calculator.Calculate(_resolvedGames);

        statistic[0].ExerciseCount.Should().Be(exerciseCount);
        statistic[0].CorrectAnswersPercentage.Should().Be(firstGameCorrectAnswersPercentage);
        statistic[0].GameDuration.Should().Be(gameDuration);

        statistic[1].ExerciseCount.Should().Be(exerciseCount);
        statistic[1].CorrectAnswersPercentage.Should().Be(secondGameCorrectAnswersPercentage);
        statistic[1].GameDuration.Should().Be(gameDuration);

        statistic[2].ExerciseCount.Should().Be(exerciseCount);
        statistic[2].CorrectAnswersPercentage.Should().Be(thirdGameCorrectAnswersPercentage);
        statistic[2].GameDuration.Should().Be(gameDuration);
    }

    [Test]
    public void Calculate_Should_ReturnEmptyGameStatistic_When_ReceivesEmptyResolvedGames()
    {
        var statistic = _calculator.Calculate(new List<ResolvedGame>());

        statistic.Should().BeEmpty();
    }

    [Test]
    public void UpdateCalculations_Should_ReturnUpdatedGameStatistic_When_ReceivesNewResolvedGames()
    {
        var gameStatistic = new List<GameStatistic>()
        {
            new(52.41, 45, DateTime.Now, TimeSpan.FromMinutes(40)),
            new(90, 10, DateTime.Now, TimeSpan.FromMinutes(9))
        };

        var statistic = _calculator.UpdateCalculations(_resolvedGames, gameStatistic);

        statistic.Count.Should().Be(5);

        statistic[0].CorrectAnswersPercentage.Should().Be(52.41);
        statistic[0].ExerciseCount.Should().Be(45);

        statistic[1].CorrectAnswersPercentage.Should().Be(90);
        statistic[1].ExerciseCount.Should().Be(10);

        statistic[2].CorrectAnswersPercentage.Should().Be(75d);
        statistic[2].ExerciseCount.Should().Be(4);

        statistic[3].CorrectAnswersPercentage.Should().Be(50d);
        statistic[3].ExerciseCount.Should().Be(4);

        statistic[4].CorrectAnswersPercentage.Should().Be(25d);
        statistic[4].ExerciseCount.Should().Be(4);
    }

    [Test]
    public void UpdateCalculations_Should_ReturnOldGameStatistic_When_DoesNotReceivesNewResolvedGames()
    {
        var gameStatistic = new List<GameStatistic>()
        {
            new(67.8, 56, DateTime.Now, TimeSpan.FromMinutes(50)),
            new(50, 8, DateTime.Now, TimeSpan.FromMinutes(4))
        };

        var statistic = _calculator.UpdateCalculations(new List<ResolvedGame>(), gameStatistic);

        statistic.Count.Should().Be(2);

        statistic[0].CorrectAnswersPercentage.Should().Be(67.8);
        statistic[0].ExerciseCount.Should().Be(56);

        statistic[1].CorrectAnswersPercentage.Should().Be(50);
        statistic[1].ExerciseCount.Should().Be(8);
    }
}