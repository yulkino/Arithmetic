using Domain.Entity;

namespace Application.Mediators.UserMediator.Get;

public sealed record GetUserQuery(Guid UserId) : IOperationRequest<User>;