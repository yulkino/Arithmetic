using API.DTOs.SettingsDtos.GetSettingsDtos;

namespace API.DTOs.StatisticDtos;

public sealed record OperationsStatisticDto(
    OperationDto Operation,
    double GameAverageDuration);