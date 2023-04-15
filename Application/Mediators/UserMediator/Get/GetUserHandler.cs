using Application.ServiceContracts.Repositories.Read;
using Domain.Entity;
using ErrorOr;
using MediatR;

namespace Application.Mediators.UserMediator.Get;

public class GetUserHandler : IRequestHandler<GetUserQuery, ErrorOr<User>>
{
    private readonly IUserReadRepository _userReadRepository;

    public GetUserHandler(IUserReadRepository userReadRepository) => _userReadRepository = userReadRepository;

    public async Task<ErrorOr<User>> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        (var login, var password) = request;

        if (await _userReadRepository.GetUserByLoginAsync(login, cancellationToken) is null)
            return Error.NotFound("User.NotFound", "User does not exists.");

        var user = await _userReadRepository.LoginUserAsync(login, password, cancellationToken);
        if (user is null)
            return Error.Failure("User.Failure", "Failed login attempt.");
        return user;
    }
}