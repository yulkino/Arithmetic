namespace API.DTOs.GameDtos;

public record ExerciseDto(
    Guid Id,
    double FirstOperand,
    int Operation,
    double SecondOperand);