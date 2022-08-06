using API.DTOs.GameDtos;

namespace API.DTOs.ResolvedGameDtos;

public sealed record ExerciseResultDto(
    Guid Id,
    double FirstOperand,
    int Operation,
    double SecondOperand,
    double Answer,
    double CorrectAnswer,
    double ExerciseResolveDuration) : ExerciseDto(Id, FirstOperand, Operation, SecondOperand);