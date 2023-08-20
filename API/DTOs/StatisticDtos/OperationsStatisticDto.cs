using API.DTOs.SettingsDtos.GetSettingsDtos;

namespace API.DTOs.StatisticDtos;

public sealed class OperationsStatisticDto
{
    public OperationDto? Operation { get; set; }
    public TimeOnly GameAverageDuration { get; set; }
}