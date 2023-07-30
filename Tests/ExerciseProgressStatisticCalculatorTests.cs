using Domain.Entity.GameEntities;
using Domain.StatisticStaff;
using FluentAssertions;

namespace Tests;

public class ExerciseProgressStatisticCalculatorTests
{
    private ExerciseProgressStatisticCalculator _calculator;
    private List<ResolvedGame> _testResolvedGames;
    private Diagram<ExerciseProgressStatistic, DateTime, TimeSpan> _oldStatisticWithoutIntersectingDate;
    private Diagram<ExerciseProgressStatistic, DateTime, TimeSpan> _oldStatisticWithIntersectingDate;

    [SetUp]
    public void Setup()
    {
        _calculator = new ExerciseProgressStatisticCalculator();
        _testResolvedGames = TestData.GetTestResolvedGame();
        _oldStatisticWithoutIntersectingDate = TestData.GetTestOldExerciseProgressStatisticWithoutIntersectingDate();
        _oldStatisticWithIntersectingDate = TestData.GetTestOldExerciseProgressStatisticWithIntersectingDate();
    }

    [Test]
    public async Task Calculate_Should_ReturnExerciseProgressStatistic_When_ReceivesResolvedGames()
    {
        var averageTimeForToday = (TimeSpan.FromSeconds(2) + TimeSpan.FromSeconds(15) + 
                                   TimeSpan.FromSeconds(8) + TimeSpan.FromSeconds(30)) / 4;
        var averageTimeForOneDayAgo = (TimeSpan.FromSeconds(11) + TimeSpan.FromSeconds(4) +
                                       TimeSpan.FromSeconds(27) + TimeSpan.FromSeconds(43)) / 4;
        var averageTimeForTwoDaysAgo = (TimeSpan.FromSeconds(38) + TimeSpan.FromSeconds(6) +
                                       TimeSpan.FromSeconds(37) + TimeSpan.FromSeconds(59)) / 4;

        var statistic = (await _calculator.Calculate(_testResolvedGames, CancellationToken.None)).ToList();

        var statisticForToday = statistic.Single(s => s.X.Equals(DateTime.Today));
        statisticForToday.Y.Should().Be(averageTimeForToday);

        var statisticOneDayAgo = statistic.Single(s => s.X.Equals(DateTime.Today.AddDays(-1)));
        statisticOneDayAgo.Y.Should().Be(averageTimeForOneDayAgo);

        var statisticTwoDayAgo = statistic.Single(s => s.X.Equals(DateTime.Today.AddDays(-2)));
        statisticTwoDayAgo.Y.Should().Be(averageTimeForTwoDaysAgo);
    }

    [Test]
    public async Task Calculate_Should_ReturnEmptyExerciseProgressStatistic_When_ReceivesEmptyResolvedGames()
    {
        var statistic = (await _calculator.Calculate(new List<ResolvedGame>(), CancellationToken.None)).ToList();

        statistic.Should().BeEmpty();
    }

    [Test]
    public async Task UpdateCalculations_Should_ReturnUpdatedExerciseProgressStatistic_When_ReceivesNewResolvedGamesWithoutIntersectingDate()
    {
        var averageTimeForToday = (TimeSpan.FromSeconds(2) + TimeSpan.FromSeconds(15) +
                                   TimeSpan.FromSeconds(8) + TimeSpan.FromSeconds(30)) / 4;
        var averageTimeForOneDayAgo = (TimeSpan.FromSeconds(11) + TimeSpan.FromSeconds(4) +
                                       TimeSpan.FromSeconds(27) + TimeSpan.FromSeconds(43)) / 4;
        var averageTimeForTwoDaysAgo = (TimeSpan.FromSeconds(38) + TimeSpan.FromSeconds(6) +
                                        TimeSpan.FromSeconds(37) + TimeSpan.FromSeconds(59)) / 4;

        var statistic = (await _calculator.UpdateCalculations(_testResolvedGames, _oldStatisticWithoutIntersectingDate, 
            CancellationToken.None)).ToList();

        var statisticForToday = statistic.Single(s => s.X.Equals(DateTime.Today));
        statisticForToday.Y.Should().Be(averageTimeForToday);

        var statisticOneDayAgo = statistic.Single(s => s.X.Equals(DateTime.Today.AddDays(-1)));
        statisticOneDayAgo.Y.Should().Be(averageTimeForOneDayAgo);

        var statisticTwoDayAgo = statistic.Single(s => s.X.Equals(DateTime.Today.AddDays(-2)));
        statisticTwoDayAgo.Y.Should().Be(averageTimeForTwoDaysAgo);

        var statisticThreeDayAgo = statistic.Single(s => s.X.Equals(DateTime.Today.AddDays(-3)));
        statisticThreeDayAgo.Y.Should().Be(TimeSpan.FromSeconds(6));

        var statisticFourDayAgo = statistic.Single(s => s.X.Equals(DateTime.Today.AddDays(-4)));
        statisticFourDayAgo.Y.Should().Be(TimeSpan.FromSeconds(5));
    }

    [Test]
    public async Task UpdateCalculations_Should_ReturnUpdatedExerciseProgressStatistic_When_ReceivesNewResolvedGamesWithIntersectingDate()
    {
        var averageTimeForToday = (TimeSpan.FromSeconds(2) + TimeSpan.FromSeconds(15) +
                                   TimeSpan.FromSeconds(8) + TimeSpan.FromSeconds(30) + 
                                   TimeSpan.FromSeconds(3) * 40) / (4 + 40);
        var averageTimeForOneDayAgo = (TimeSpan.FromSeconds(11) + TimeSpan.FromSeconds(4) +
                                       TimeSpan.FromSeconds(27) + TimeSpan.FromSeconds(43)) / 4;
        var averageTimeForTwoDaysAgo = (TimeSpan.FromSeconds(38) + TimeSpan.FromSeconds(6) +
                                        TimeSpan.FromSeconds(37) + TimeSpan.FromSeconds(59)) / 4;

        var statistic = (await _calculator.UpdateCalculations(_testResolvedGames, 
            _oldStatisticWithIntersectingDate, CancellationToken.None)).ToList();

        var statisticForToday = statistic.Single(s => s.X.Equals(DateTime.Today));
        statisticForToday.Y.Should().Be(averageTimeForToday);

        var statisticOneDayAgo = statistic.Single(s => s.X.Equals(DateTime.Today.AddDays(-1)));
        statisticOneDayAgo.Y.Should().Be(averageTimeForOneDayAgo);

        var statisticTwoDayAgo = statistic.Single(s => s.X.Equals(DateTime.Today.AddDays(-2)));
        statisticTwoDayAgo.Y.Should().Be(averageTimeForTwoDaysAgo);

        var statisticThreeDayAgo = statistic.Single(s => s.X.Equals(DateTime.Today.AddDays(-3)));
        statisticThreeDayAgo.Y.Should().Be(TimeSpan.FromSeconds(6));

        var statisticFourDayAgo = statistic.Single(s => s.X.Equals(DateTime.Today.AddDays(-4)));
        statisticFourDayAgo.Y.Should().Be(TimeSpan.FromSeconds(5));
    }

    [Test]
    public async Task UpdateCalculations_Should_ReturnOldExerciseProgressStatistic_When_ReceivesEmptyResolvedGames()
    {
        var statistic = (await _calculator.UpdateCalculations(new List<ResolvedGame>(), 
            _oldStatisticWithoutIntersectingDate, CancellationToken.None)).ToList();

        var statisticThreeDayAgo = statistic.Single(s => s.X.Equals(DateTime.Today.AddDays(-3)));
        statisticThreeDayAgo.Y.Should().Be(TimeSpan.FromSeconds(6));

        var statisticFourDayAgo = statistic.Single(s => s.X.Equals(DateTime.Today.AddDays(-4)));
        statisticFourDayAgo.Y.Should().Be(TimeSpan.FromSeconds(5));
    }
}