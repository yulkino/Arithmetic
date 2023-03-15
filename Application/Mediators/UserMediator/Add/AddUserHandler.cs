using Application.ServiceContracts.Repositories.Read;
using Application.ServiceContracts.Repositories.Write;
using Domain.Entity;
using ErrorOr;
using MediatR;

namespace Application.Mediators.UserMediator.Add;

public class AddUserHandler : IRequestHandler<AddUserCommand, ErrorOr<User>>
{
    private readonly IUserWriteRepository _userWriteRepository;
    private readonly IUserReadRepository _userReadRepository;

    public AddUserHandler(IUserWriteRepository userWriteRepository, IUserReadRepository userReadRepository)
    {
        _userWriteRepository = userWriteRepository;
        _userReadRepository = userReadRepository;
    }

    public async Task<ErrorOr<User>> Handle(AddUserCommand request, CancellationToken cancellationToken)
    {
        var (login, password, passwordConfirmation) = request;

        if (await _userReadRepository.GetUserByLoginAsync(login, cancellationToken) is not null)
            return Error.Conflict("User.Conflict", $"User with Login {login} already exists.");

        return await _userWriteRepository.AddUserAsync(login, password, cancellationToken);
    }
}
