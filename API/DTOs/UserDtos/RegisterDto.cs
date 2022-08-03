namespace API.DTOs.UserDtos;

public sealed record RegisterDto(
    string Login,
    string PasswordHash,
    string PasswordHashConfirmation);