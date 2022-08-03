namespace API.DTOs.StatisticDtos;

public sealed record OperationsStatisticDto(
    int Operation,
    double GameAverageDuration);