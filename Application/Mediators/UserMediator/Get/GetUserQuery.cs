using Domain.Entity;

namespace Application.Mediators.UserMediator.Get;

public sealed record GetUserQuery(string Login, string Password) : IOperationRequest<User>;