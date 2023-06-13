using Domain.Entity.ExerciseEntities;
using Domain.Entity.GameEntities;
using Domain.Entity.SettingsEntities;
using Domain.Entity;
using Application.Services.StatisticServices;
using FluentAssertions;
using Domain.StatisticStaff;

namespace Tests;

public class OperationsStatisticCalculatorTests
{
    private OperationsStatisticCalculator _calculator;
    private List<ResolvedGame> _resolvedGames;
    private Diagram<OperationsStatistic, Operation, TimeSpan> _oldStatistic;

    [SetUp]
    public void Setup()
    {
        _calculator = new OperationsStatisticCalculator();
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
                new(5, TimeSpan.FromSeconds(15), new Exercise(35, 35, Operation.Division)), //-
                new(350, TimeSpan.FromSeconds(8), new Exercise(10, 35, Operation.Multiplication)), //+
                new(-25, TimeSpan.FromSeconds(30), new Exercise(10, 35, Operation.Subtraction)) //+
            });

        _resolvedGames[1].ResolvedExercises
            .AddRange(new List<ResolvedExercise>()
            {
                new(200, TimeSpan.FromSeconds(11), new Exercise(100, 100, Operation.Addition)), //+
                new(30, TimeSpan.FromSeconds(4), new Exercise(5, 35, Operation.Subtraction)), //-
                new(350, TimeSpan.FromSeconds(27), new Exercise(10, 35, Operation.Multiplication)), //+
                new(10, TimeSpan.FromSeconds(43), new Exercise(100, 50, Operation.Subtraction)) //-
            });

        _resolvedGames[2].ResolvedExercises
            .AddRange(new List<ResolvedExercise>()
            {
                new(300, TimeSpan.FromSeconds(38), new Exercise(10, 35, Operation.Multiplication)), //-
                new(5, TimeSpan.FromSeconds(6), new Exercise(444, 4, Operation.Division)), //-
                new(35, TimeSpan.FromSeconds(37), new Exercise(10, 35, Operation.Multiplication)), //-
                new(20, TimeSpan.FromSeconds(59), new Exercise(200, 10, Operation.Division)) //+
            });

        _resolvedGames.ForEach(r => r.ProcessGameResult());

        _oldStatistic = new Diagram<OperationsStatistic, Operation, TimeSpan>();
        _oldStatistic.AddNode(new OperationsStatistic(Operation.Addition, TimeSpan.FromSeconds(4), 3));
        _oldStatistic.AddNode(new OperationsStatistic(Operation.Subtraction, TimeSpan.FromSeconds(10), 4));
        _oldStatistic.AddNode(new OperationsStatistic(Operation.Division, TimeSpan.FromSeconds(21), 2));
        _oldStatistic.AddNode(new OperationsStatistic(Operation.Multiplication, TimeSpan.FromSeconds(17), 3));
    }

    [Test]
    public void Calculate_Should_ReturnOperationsStatistic_When_ReceivesResolvedGames()
    {
        var averageTimeForAddition = (TimeSpan.FromSeconds(11) + TimeSpan.FromSeconds(2)) / 2;
        var averageTimeForSubtraction = (TimeSpan.FromSeconds(30) + TimeSpan.FromSeconds(4) + TimeSpan.FromSeconds(43)) / 3;
        var averageTimeForDivision = (TimeSpan.FromSeconds(15) + TimeSpan.FromSeconds(6) + TimeSpan.FromSeconds(59)) / 3;
        var averageTimeForMultiplication = (TimeSpan.FromSeconds(8) + TimeSpan.FromSeconds(27) + 
            TimeSpan.FromSeconds(38) + TimeSpan.FromSeconds(37)) / 4;

        var statistic = _calculator.Calculate(_resolvedGames).ToList();

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
    public void Calculate_Should_ReturnEmptyOperationsStatistic_When_ReceivesEmptyResolvedGames()
    {
        var statistic = _calculator.Calculate(new List<ResolvedGame>()).ToList();

        statistic.Count.Should().Be(0);
    }

    [Test]
    public void UpdateCalculations_Should_ReturnUpdatedOperationsStatistic_When_ReceivesNewResolvedGames()
    {
        var user = new User("TestLogin", "TestPassword");
        var settings = new Settings(
            Difficulty.Medium,
            new HashSet<Operation>()
            {
                Operation.Addition, Operation.Division, Operation.Subtraction
            },
            3);
        var newResolvedGame = new List<ResolvedGame>()
        {
            new(new Game(user, settings)),
            new(new Game(user, settings))
        };
        newResolvedGame[0].ResolvedExercises
            .AddRange(new List<ResolvedExercise>()
            {
                new(111, TimeSpan.FromSeconds(49), new Exercise(11, 11, Operation.Addition)), //-
                new(6, TimeSpan.FromSeconds(5), new Exercise(36, 6, Operation.Division)), //+
                new(-25, TimeSpan.FromSeconds(21), new Exercise(10, 35, Operation.Subtraction)) //+
            });
        newResolvedGame[1].ResolvedExercises
            .AddRange(new List<ResolvedExercise>()
            {
                new(61, TimeSpan.FromSeconds(10), new Exercise(11, 50, Operation.Addition)), //+
                new(10, TimeSpan.FromSeconds(7), new Exercise(50, 5, Operation.Division)), //+
                new(45, TimeSpan.FromSeconds(9), new Exercise(10, 35, Operation.Addition)) //+
            });

        var averageTimeForNewAddition = (TimeSpan.FromSeconds(49) + TimeSpan.FromSeconds(10) + TimeSpan.FromSeconds(9)) / 3;
        var averageTimeForNewDivision = (TimeSpan.FromSeconds(5) + TimeSpan.FromSeconds(7)) / 2; 
        var averageTimeForNewSubtraction = TimeSpan.FromSeconds(21);

        var oldStatisticList = _oldStatistic.ToList();
        var newAverageForAllAddition = (averageTimeForNewAddition * 3 + oldStatisticList[0].Y * 3) / (3 + oldStatisticList[0].ElementCountStatistic);
        var newAverageForAllSubtraction = (averageTimeForNewSubtraction + oldStatisticList[1].Y * 4) / (1 + oldStatisticList[1].ElementCountStatistic);
        var newAverageForAllDivision = (averageTimeForNewDivision * 2 + oldStatisticList[2].Y * 2) / (2 + oldStatisticList[2].ElementCountStatistic);
        var newAverageForAllMultiplication = oldStatisticList[3].Y;

        var statistic = _calculator.UpdateCalculations(newResolvedGame, _oldStatistic).ToList();

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
    public void UpdateCalculations_Should_ReturnOldOperationsStatistic_When_ReceivesEmptyResolvedGames()
    {
        var oldAddition = _oldStatistic.Single(o => o.X == Operation.Addition);
        var oldSubtraction = _oldStatistic.Single(o => o.X == Operation.Subtraction);
        var oldDivision = _oldStatistic.Single(o => o.X == Operation.Division);
        var oldMultiplication = _oldStatistic.Single(o => o.X == Operation.Multiplication);

        var statistic = _calculator.UpdateCalculations(new List<ResolvedGame>(), _oldStatistic).ToList();

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
    public void Calculate_Should_ReturnOperationsAllOperationsStatistic_When_ReceivesNotAllOperationsFromResolvedGames()
    {
        var user = new User("TestLogin", "TestPassword");
        var settings = new Settings(
            Difficulty.Medium,
            new HashSet<Operation>()
            {
                Operation.Addition, Operation.Subtraction
            },
            3);
        var resolvedGame = new List<ResolvedGame>()
        {
            new(new Game(user, settings)),
            new(new Game(user, settings))
        };
        resolvedGame[0].ResolvedExercises
            .AddRange(new List<ResolvedExercise>()
            {
                new(111, TimeSpan.FromSeconds(49), new Exercise(11, 11, Operation.Addition)), //-
                new(42, TimeSpan.FromSeconds(34), new Exercise(36, 6, Operation.Subtraction)), //+
                new(-25, TimeSpan.FromSeconds(21), new Exercise(10, 35, Operation.Subtraction)) //+
            });
        resolvedGame[1].ResolvedExercises
            .AddRange(new List<ResolvedExercise>()
            {
                new(61, TimeSpan.FromSeconds(10), new Exercise(11, 50, Operation.Addition)), //+
                new(10, TimeSpan.FromSeconds(7), new Exercise(50, 5, Operation.Subtraction)), //-
                new(45, TimeSpan.FromSeconds(9), new Exercise(10, 35, Operation.Addition)) //+
            });

        var averageTimeForAddition = (TimeSpan.FromSeconds(49) + TimeSpan.FromSeconds(10) + TimeSpan.FromSeconds(9)) / 3;
        var averageTimeForSubtraction = (TimeSpan.FromSeconds(34) + TimeSpan.FromSeconds(21) + TimeSpan.FromSeconds(7)) / 3;

        var statistic = _calculator.Calculate(resolvedGame).ToList();

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