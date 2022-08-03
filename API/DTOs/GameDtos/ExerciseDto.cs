namespace API.DTOs.GameDtos;

public record ExerciseDto(
    double FirstOperand,
    int Operation,
    double SecondOperand);