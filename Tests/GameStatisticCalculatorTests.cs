using Domain.Entity.GameEntities;
using Domain.StatisticStaff;
using FluentAssertions;

namespace Tests;

public class GameStatisticCalculatorTests
{
    private GameStatisticCalculator _calculator;
    private List<ResolvedGame> _testResolvedGames;

    [SetUp]
    public void Setup()
    {
        _calculator = new GameStatisticCalculator();
        _testResolvedGames = TestData.GetTestResolvedGame();
    }

    [Test]
    public async Task Calculate_Should_ReturnGameStatistic_When_ReceivesResolvedGames()
    {
        var exerciseCount = 4;
        var firstGameCorrectAnswersPercentage = 75d;
        var secondGameCorrectAnswersPercentage = 50d;
        var thirdGameCorrectAnswersPercentage = 25d;

        var statistic = await _calculator.Calculate(_testResolvedGames, CancellationToken.None);

        statistic[0].ExerciseCount.Should().Be(exerciseCount);
        statistic[0].CorrectAnswersPercentage.Should().Be(firstGameCorrectAnswersPercentage);

        statistic[1].ExerciseCount.Should().Be(exerciseCount);
        statistic[1].CorrectAnswersPercentage.Should().Be(secondGameCorrectAnswersPercentage);

        statistic[2].ExerciseCount.Should().Be(exerciseCount);
        statistic[2].CorrectAnswersPercentage.Should().Be(thirdGameCorrectAnswersPercentage);
    }

    [Test]
    public async Task Calculate_Should_ReturnEmptyGameStatistic_When_ReceivesEmptyResolvedGames()
    {
        var statistic = await _calculator.Calculate(new List<ResolvedGame>(), CancellationToken.None);

        statistic.Should().BeEmpty();
    }

    [Test]
    public async Task UpdateCalculations_Should_ReturnUpdatedGameStatistic_When_ReceivesNewResolvedGames()
    {
        var gameStatistic = new List<GameStatistic>()
        {
            new(52.41, 45, DateTime.Now, TimeSpan.FromMinutes(40)),
            new(90, 10, DateTime.Now, TimeSpan.FromMinutes(9))
        };

        var statistic = await _calculator.UpdateCalculations(_testResolvedGames, gameStatistic, CancellationToken.None);

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
    public async Task UpdateCalculations_Should_ReturnOldGameStatistic_When_DoesNotReceivesNewResolvedGames()
    {
        var gameStatistic = new List<GameStatistic>()
        {
            new(67.8, 56, DateTime.Now, TimeSpan.FromMinutes(50)),
            new(50, 8, DateTime.Now, TimeSpan.FromMinutes(4))
        };

        var statistic = await _calculator.UpdateCalculations(new List<ResolvedGame>(), gameStatistic, CancellationToken.None);

        statistic.Count.Should().Be(2);

        statistic[0].CorrectAnswersPercentage.Should().Be(67.8);
        statistic[0].ExerciseCount.Should().Be(56);

        statistic[1].CorrectAnswersPercentage.Should().Be(50);
        statistic[1].ExerciseCount.Should().Be(8);
    }
}