using Domain.Entity;

namespace Application.Mediators.UserMediator.Add;

public sealed record AddUserCommand(
    string Login,
    string PasswordHash,
    string PasswordHashConfirmation) : IOperationRequest<User>;