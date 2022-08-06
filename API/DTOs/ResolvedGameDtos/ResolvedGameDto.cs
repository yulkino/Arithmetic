namespace API.DTOs.ResolvedGameDtos;

public sealed record ResolvedGameDto(
    int CorrectAnswerCount,
    TimeSpan ElapsedTime,
    List<ExerciseResultDto> ResolvedExercises);