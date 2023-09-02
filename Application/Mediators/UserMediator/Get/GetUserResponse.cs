namespace Application.Mediators.UserMediator.Get;

public record GetUserResponse(Guid Id, string Email, string? JwtToken);