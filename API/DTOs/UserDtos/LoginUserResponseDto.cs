namespace API.DTOs.UserDtos;

public sealed record LoginUserResponseDto(Guid Id, string Email, string JwtToken);