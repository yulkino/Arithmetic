﻿namespace Domain.StatisticStaff;

public sealed class GameStatistic
{
    public Guid Id { get; init; }
    public DateTime GameDate { get; set; }
    public int ExerciseCount { get; set; }
    public TimeSpan GameDuration { get; set; }
    public double CorrectAnswersPercentage { get; set; }

    public GameStatistic() => Id = Guid.NewGuid();
}