using API.DTOs.GameDtos;

namespace API.DTOs.ResultDtos;

public sealed record ExerciseResultDto(
    double FirstOperand,
    int Operation,
    double SecondOperand,
    double Answer,
    double CorrectAnswer,
    double ExerciseResolveDuration) : ExerciseDto(FirstOperand, Operation, SecondOperand);