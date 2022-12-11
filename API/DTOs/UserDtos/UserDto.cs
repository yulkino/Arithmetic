namespace API.DTOs.UserDtos;

public sealed record UserDto(Guid Id, string Login, string Password);