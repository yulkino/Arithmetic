namespace API.DTOs.ResolvedGameDtos;

public sealed record ResolvedExerciseDto(
    double UserAnswer,
    bool IsCorrect,
    double CorrectAnswer,
    TimeSpan ElapsedTime);