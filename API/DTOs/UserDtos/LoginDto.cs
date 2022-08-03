namespace API.DTOs.UserDtos;

public sealed record LoginDto(
    string Login,
    string PasswordHash);