using API.DTOs.GameDtos;
using API.DTOs.SettingsDtos.GetSettingsDtos;

namespace API.DTOs.ResolvedGameDtos;

public sealed record ResolvedExerciseDto(
    Guid Id,
    double FirstOperand,
    OperationDto Operation,
    double SecondOperand,
    double UserAnswer,
    bool IsCorrect,
    double CorrectAnswer,
    double ElapsedTime) : ExerciseDto(Id, FirstOperand, Operation, SecondOperand);