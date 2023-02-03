using API.DTOs.GameDtos;

namespace API.DTOs.ResolvedGameDtos;

public sealed record ResolvedExerciseDto(
    Guid Id,
    double FirstOperand,
    int Operation,
    double SecondOperand,
    double Answer,
    double CorrectAnswer,
    double ExerciseResolveDuration) : ExerciseDto(Id, FirstOperand, Operation, SecondOperand);