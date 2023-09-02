namespace API.DTOs.UserDtos;

public sealed record RegisterDto(
    string Email,
    string Password,
    string PasswordConfirmation);