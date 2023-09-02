using Application.ServiceContracts.Repositories.Read.SettingsReadRepositories;
using Domain.Entity.ExerciseEntities;
using Domain.Entity.GameEntities;
using Domain.Entity.SettingsEntities;
using Domain.Entity;
using Domain.StatisticStaff;
using FluentAssertions;
using Moq;

namespace Tests;

public class OperationsStatisticCalculatorTests
{
    private OperationsStatisticCalculator _calculator;
    private List<ResolvedGame> _testResolvedGames;
    private Diagram<OperationsStatistic, Operation, TimeSpan> _testOldStatistic;

    [SetUp]
    public void Setup()
    {
        var operationReadRepository = new Mock<IOperationsReadRepository>();
        operationReadRepository.Setup(r => r.GetOperationsAsync(It.IsAny<CancellationToken>())).ReturnsAsync(
            new HashSet<Operation>()
            {
                Operation.Subtraction,
                Operation.Addition,
                Operation.Division,
                Operation.Multiplication
            });
        _calculator = new OperationsStatisticCalculator(operationReadRepository.Object);
        _testResolvedGames = TestData.GetTestResolvedGame();
        _testOldStatistic = TestData.GetTestOldOperationsStatistic();
    }

    [Test]
    public async Task Calculate_Should_ReturnOperationsStatistic_When_ReceivesResolvedGames()
    {
        var averageTimeForAddition = (TimeSpan.FromSeconds(11) + TimeSpan.FromSeconds(2)) / 2;
        var averageTimeForSubtraction = (TimeSpan.FromSeconds(30) + TimeSpan.FromSeconds(4) + TimeSpan.FromSeconds(43)) / 3;
        var averageTimeForDivision = (TimeSpan.FromSeconds(15) + TimeSpan.FromSeconds(6) + TimeSpan.FromSeconds(59)) / 3;
        var averageTimeForMultiplication = (TimeSpan.FromSeconds(8) + TimeSpan.FromSeconds(27) + 
            TimeSpan.FromSeconds(38) + TimeSpan.FromSeconds(37)) / 4;

        var statistic = (await _calculator.Calculate(_testResolvedGames, CancellationToken.None)).ToList();

        statistic.Count.Should().Be(4);

        var additionStatistic = statistic.Single(s => s.X == Operation.Addition);
        additionStatistic.Y.Should().Be(averageTimeForAddition);
        additionStatistic.ElementCountStatistic.Should().Be(2);

        var subtractionStatistic = statistic.Single(s => s.X == Operation.Subtraction);
        subtractionStatistic.Y.Should().Be(averageTimeForSubtraction);
        subtractionStatistic.ElementCountStatistic.Should().Be(3);

        var divisionStatistic = statistic.Single(s => s.X == Operation.Division);
        divisionStatistic.Y.Should().Be(averageTimeForDivision);
        divisionStatistic.ElementCountStatistic.Should().Be(3);

        var multiplicationStatistic = statistic.Single(s => s.X == Operation.Multiplication);
        multiplicationStatistic.Y.Should().Be(averageTimeForMultiplication);
        multiplicationStatistic.ElementCountStatistic.Should().Be(4);
    }

    [Test]
    public async Task Calculate_Should_ReturnEmptyOperationsStatistic_When_ReceivesEmptyResolvedGames()
    {
        var statistic = (await _calculator.Calculate(new List<ResolvedGame>(), CancellationToken.None)).ToList();

        statistic.Should().BeEmpty();
    }

    [Test]
    public async Task UpdateCalculations_Should_ReturnUpdatedOperationsStatistic_When_ReceivesNewResolvedGames()
    {
        var user = new User("TestLogin", "TestPassword","TestIdentity");
        var settings = new Settings(
            Difficulty.Medium,
            new HashSet<Operation>()
            {
                Operation.Addition, Operation.Division, Operation.Subtraction
            },
            3);
        var newTestResolvedGame = new List<ResolvedGame>()
        {
            new(new Game(user, settings, DateTime.Now)),
            new(new Game(user, settings, DateTime.Now))
        };
        var startTime = DateTime.Now;
        newTestResolvedGame[0].ResolvedExercises
            .AddRange(new List<ResolvedExercise>()
            {
                new(111, TimeSpan.FromSeconds(49), 
                    new Exercise(11, 11, Operation.Addition, startTime)), //-
                new(6, TimeSpan.FromSeconds(5), 
                    new Exercise(36, 6, Operation.Division, startTime)), //+
                new(-25, TimeSpan.FromSeconds(21), 
                    new Exercise(10, 35, Operation.Subtraction, startTime)) //+
            });
        newTestResolvedGame[1].ResolvedExercises
            .AddRange(new List<ResolvedExercise>()
            {
                new(61, TimeSpan.FromSeconds(10), 
                    new Exercise(11, 50, Operation.Addition, startTime)), //+
                new(10, TimeSpan.FromSeconds(7), 
                    new Exercise(50, 5, Operation.Division, startTime)), //+
                new(45, TimeSpan.FromSeconds(9), 
                    new Exercise(10, 35, Operation.Addition, startTime)) //+
            });
        newTestResolvedGame.ForEach(x => x.ProcessGameResult());

        var averageTimeForNewAddition = (TimeSpan.FromSeconds(49) + TimeSpan.FromSeconds(10) + TimeSpan.FromSeconds(9)) / 3;
        var averageTimeForNewDivision = (TimeSpan.FromSeconds(5) + TimeSpan.FromSeconds(7)) / 2; 
        var averageTimeForNewSubtraction = TimeSpan.FromSeconds(21);

        var oldStatisticList = _testOldStatistic.ToList();
        var newAverageForAllAddition = (averageTimeForNewAddition * 3 + oldStatisticList[0].Y * 3) / 
                                       (3 + oldStatisticList[0].ElementCountStatistic);
        var newAverageForAllSubtraction = (averageTimeForNewSubtraction + oldStatisticList[1].Y * 4) / 
                                          (1 + oldStatisticList[1].ElementCountStatistic);
        var newAverageForAllDivision = (averageTimeForNewDivision * 2 + oldStatisticList[2].Y * 2) / 
                                       (2 + oldStatisticList[2].ElementCountStatistic);
        var newAverageForAllMultiplication = oldStatisticList[3].Y;

        var statistic = (await _calculator.UpdateCalculations(newTestResolvedGame, _testOldStatistic, CancellationToken.None)).ToList();

        statistic.Count.Should().Be(4);

        var additionStatistic = statistic.Single(s => s.X == Operation.Addition);
        additionStatistic.Y.Should().Be(newAverageForAllAddition);
        additionStatistic.ElementCountStatistic.Should().Be(6);

        var subtractionStatistic = statistic.Single(s => s.X == Operation.Subtraction);
        subtractionStatistic.Y.Should().Be(newAverageForAllSubtraction);
        subtractionStatistic.ElementCountStatistic.Should().Be(5);

        var divisionStatistic = statistic.Single(s => s.X == Operation.Division);
        divisionStatistic.Y.Should().Be(newAverageForAllDivision);
        divisionStatistic.ElementCountStatistic.Should().Be(4);

        var multiplicationStatistic = statistic.Single(s => s.X == Operation.Multiplication);
        multiplicationStatistic.Y.Should().Be(newAverageForAllMultiplication);
        multiplicationStatistic.ElementCountStatistic.Should().Be(3);
    }

    [Test]
    public async Task UpdateCalculations_Should_ReturnOldOperationsStatistic_When_ReceivesEmptyResolvedGames()
    {
        var oldAddition = _testOldStatistic.Single(o => o.X == Operation.Addition);
        var oldSubtraction = _testOldStatistic.Single(o => o.X == Operation.Subtraction);
        var oldDivision = _testOldStatistic.Single(o => o.X == Operation.Division);
        var oldMultiplication = _testOldStatistic.Single(o => o.X == Operation.Multiplication);

        var statistic = (await _calculator.UpdateCalculations(new List<ResolvedGame>(), 
            _testOldStatistic, CancellationToken.None)).ToList();

        statistic.Count.Should().Be(4);

        var additionStatistic = statistic.Single(s => s.X == Operation.Addition);
        additionStatistic.Y.Should().Be(oldAddition.Y);
        additionStatistic.ElementCountStatistic.Should().Be(oldAddition.ElementCountStatistic);

        var subtractionStatistic = statistic.Single(s => s.X == Operation.Subtraction);
        subtractionStatistic.Y.Should().Be(oldSubtraction.Y);
        subtractionStatistic.ElementCountStatistic.Should().Be(oldSubtraction.ElementCountStatistic);

        var divisionStatistic = statistic.Single(s => s.X == Operation.Division);
        divisionStatistic.Y.Should().Be(oldDivision.Y);
        divisionStatistic.ElementCountStatistic.Should().Be(oldDivision.ElementCountStatistic);

        var multiplicationStatistic = statistic.Single(s => s.X == Operation.Multiplication);
        multiplicationStatistic.Y.Should().Be(oldMultiplication.Y);
        multiplicationStatistic.ElementCountStatistic.Should().Be(oldMultiplication.ElementCountStatistic);
    }

    [Test]
    public async Task Calculate_Should_ReturnAllOperationsStatistic_When_ReceivesNotAllOperationsFromResolvedGames()
    {
        var user = new User("TestLogin", "TestPassword", "TestIdentity");
        var settings = new Settings(
            Difficulty.Medium,
            new HashSet<Operation>()
            {
                Operation.Addition, Operation.Subtraction
            },
            3);
        var testResolvedGame = new List<ResolvedGame>()
        {
            new(new Game(user, settings, DateTime.Now)),
            new(new Game(user, settings, DateTime.Now))
        };
        var startTime = DateTime.Now;
        testResolvedGame[0].ResolvedExercises
            .AddRange(new List<ResolvedExercise>()
            {
                new(111, TimeSpan.FromSeconds(49), new Exercise(11, 11, Operation.Addition, startTime)), //-
                new(42, TimeSpan.FromSeconds(34), new Exercise(36, 6, Operation.Subtraction, startTime)), //+
                new(-25, TimeSpan.FromSeconds(21), new Exercise(10, 35, Operation.Subtraction, startTime)) //+
            });
        testResolvedGame[1].ResolvedExercises
            .AddRange(new List<ResolvedExercise>()
            {
                new(61, TimeSpan.FromSeconds(10), new Exercise(11, 50, Operation.Addition, startTime)), //+
                new(10, TimeSpan.FromSeconds(7), new Exercise(50, 5, Operation.Subtraction, startTime)), //-
                new(45, TimeSpan.FromSeconds(9), new Exercise(10, 35, Operation.Addition, startTime)) //+
            });
        testResolvedGame.ForEach(x => x.ProcessGameResult());

        var averageTimeForAddition = (TimeSpan.FromSeconds(49) + TimeSpan.FromSeconds(10) + TimeSpan.FromSeconds(9)) / 3;
        var averageTimeForSubtraction = (TimeSpan.FromSeconds(34) + TimeSpan.FromSeconds(21) + TimeSpan.FromSeconds(7)) / 3;

        var statistic = (await _calculator.Calculate(testResolvedGame, CancellationToken.None)).ToList();

        statistic.Count.Should().Be(4);

        var additionStatistic = statistic.Single(s => s.X == Operation.Addition);
        additionStatistic.Y.Should().Be(averageTimeForAddition);
        additionStatistic.ElementCountStatistic.Should().Be(3);

        var subtractionStatistic = statistic.Single(s => s.X == Operation.Subtraction);
        subtractionStatistic.Y.Should().Be(averageTimeForSubtraction);
        subtractionStatistic.ElementCountStatistic.Should().Be(3);

        var divisionStatistic = statistic.Single(s => s.X == Operation.Division);
        divisionStatistic.Y.Should().Be(TimeSpan.Zero);
        divisionStatistic.ElementCountStatistic.Should().Be(0);

        var multiplicationStatistic = statistic.Single(s => s.X == Operation.Multiplication);
        multiplicationStatistic.Y.Should().Be(TimeSpan.Zero);
        multiplicationStatistic.ElementCountStatistic.Should().Be(0);
    }
}