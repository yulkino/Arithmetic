namespace API.DTOs.UserDtos;

public sealed record LoginDto(
    string Email,
    string Password);