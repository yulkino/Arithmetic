namespace Application.Mediators.UserMediator.Get;

public sealed record GetUserQuery(string Email, string Password) : IOperationRequest<GetUserResponse>;