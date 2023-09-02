using Domain.Entity;

namespace Application.Mediators.UserMediator.Add;

public sealed record AddUserCommand(
    string Email,
    string Password,
    string PasswordConfirmation) : IOperationRequest<User>;