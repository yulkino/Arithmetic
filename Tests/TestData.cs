using Domain.Entity.ExerciseEntities;
using Domain.Entity.GameEntities;
using Domain.Entity.SettingsEntities;
using Domain.Entity;
using Domain.StatisticStaff;

namespace Tests;

public static class TestData
{
    public static List<ResolvedGame> GetTestResolvedGame()
    {
        var user = new User("TestLogin", "TestIdentity");
        var settings = new Settings(
            Difficulty.Medium,
            new HashSet<Operation>()
            {
                Operation.Addition, Operation.Division, Operation.Multiplication, Operation.Subtraction
            },
            4);
        var testResolvedGames = new List<ResolvedGame>()
        {
            new(new Game(user, settings, DateTime.Today)),
            new(new Game(user, settings, DateTime.Today.AddDays(-1))),
            new(new Game(user, settings, DateTime.Today.AddDays(-2)))
        };
        var startTime = DateTime.Now;
        testResolvedGames[0].ResolvedExercises
            .AddRange(new List<ResolvedExercise>()
            {
                new(45, TimeSpan.FromSeconds(2), new Exercise(10, 35, Operation.Addition, startTime)), //+
                new(5, TimeSpan.FromSeconds(15), new Exercise(35, 35, Operation.Division, startTime)), //-
                new(350, TimeSpan.FromSeconds(8), new Exercise(10, 35, Operation.Multiplication, startTime)), //+
                new(-25, TimeSpan.FromSeconds(30), new Exercise(10, 35, Operation.Subtraction, startTime)) //+
            });

        testResolvedGames[1].ResolvedExercises
            .AddRange(new List<ResolvedExercise>()
            {
                new(200, TimeSpan.FromSeconds(11), new Exercise(100, 100, Operation.Addition, startTime)), //+
                new(30, TimeSpan.FromSeconds(4), new Exercise(5, 35, Operation.Subtraction, startTime)), //-
                new(350, TimeSpan.FromSeconds(27), new Exercise(10, 35, Operation.Multiplication, startTime)), //+
                new(10, TimeSpan.FromSeconds(43), new Exercise(100, 50, Operation.Subtraction, startTime)) //-
            });

        testResolvedGames[2].ResolvedExercises
            .AddRange(new List<ResolvedExercise>()
            {
                new(300, TimeSpan.FromSeconds(38), new Exercise(10, 35, Operation.Multiplication, startTime)), //-
                new(5, TimeSpan.FromSeconds(6), new Exercise(444, 4, Operation.Division, startTime)), //-
                new(35, TimeSpan.FromSeconds(37), new Exercise(10, 35, Operation.Multiplication, startTime)), //-
                new(20, TimeSpan.FromSeconds(59), new Exercise(200, 10, Operation.Division, startTime)) //+
            });

        testResolvedGames.ForEach(r => r.ProcessGameResult());
        return testResolvedGames;
    }

    public static Diagram<OperationsStatistic, Operation, TimeSpan> GetTestOldOperationsStatistic()
    {
        var testOldStatistic = new Diagram<OperationsStatistic, Operation, TimeSpan>();
        testOldStatistic.AddNode(new OperationsStatistic(Operation.Addition, TimeSpan.FromSeconds(4), 3));
        testOldStatistic.AddNode(new OperationsStatistic(Operation.Subtraction, TimeSpan.FromSeconds(10), 4));
        testOldStatistic.AddNode(new OperationsStatistic(Operation.Division, TimeSpan.FromSeconds(21), 2));
        testOldStatistic.AddNode(new OperationsStatistic(Operation.Multiplication, TimeSpan.FromSeconds(17), 3));

        return testOldStatistic;
    }

    public static Diagram<ExerciseProgressStatistic, DateTime, TimeSpan> GetTestOldExerciseProgressStatisticWithoutIntersectingDate()
    {
        var testOldStatistic = new Diagram<ExerciseProgressStatistic, DateTime, TimeSpan>();
        testOldStatistic.AddNode(new ExerciseProgressStatistic(DateTime.Today.AddDays(-3), TimeSpan.FromSeconds(6), 10));
        testOldStatistic.AddNode(new ExerciseProgressStatistic(DateTime.Today.AddDays(-4), TimeSpan.FromSeconds(5), 12));
        return testOldStatistic;
    }

    public static Diagram<ExerciseProgressStatistic, DateTime, TimeSpan> GetTestOldExerciseProgressStatisticWithIntersectingDate()
    {
        var testOldStatistic = GetTestOldExerciseProgressStatisticWithoutIntersectingDate();
        testOldStatistic.AddNode(new ExerciseProgressStatistic(DateTime.Today, TimeSpan.FromSeconds(3), 40));
        return testOldStatistic;
    }
}