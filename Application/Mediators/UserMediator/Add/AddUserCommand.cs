using Domain.Entity;

namespace Application.Mediators.UserMediator.Add;

public sealed record AddUserCommand(
    string Login,
    string Password,
    string PasswordConfirmation) : IOperationRequest<User>;