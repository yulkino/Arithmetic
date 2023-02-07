using API.DTOs.SettingsDtos;

namespace API.DTOs.StatisticDtos;

public sealed record OperationsStatisticDto(
    OperationDto Operation,
    double GameAverageDuration);