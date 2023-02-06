using API.DTOs.SettingsDtos;

namespace API.DTOs.GameDtos;

public record ExerciseDto(
    Guid Id,
    double LeftOperand,
    OperationDto Operation,
    double RightOperand);