using API.DTOs.SettingsDtos.GetSettingsDtos;

namespace API.DTOs.GameDtos;

public record ExerciseDto(
    Guid Id,
    double LeftOperand,
    OperationDto Operation,
    double RightOperand);