using Application.ServiceContracts.Repositories.Read;
using Domain.Entity;
using ErrorOr;
using MediatR;

namespace Application.Mediators.UserMediator.Get;

public class GetUserHandler : IRequestHandler<GetUserQuery, ErrorOr<User>>
{
    private readonly IUserReadRepository _userReadRepository;

    public GetUserHandler(IUserReadRepository userReadRepository)
    {
        _userReadRepository = userReadRepository;
    }

    public async Task<ErrorOr<User>> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        var (login, password) = request;

        if (await _userReadRepository.GetUserByLoginAsync(login, cancellationToken) is null)
            return ErrorOr<User>.From(new List<Error> { Error.NotFound("General.NotFound", "User is not exists.") });

        var user = await _userReadRepository.LoginUserAsync(login, password, cancellationToken);
        return user ?? ErrorOr<User>.From(new List<Error> { Error.Failure("General.Failure", "Failed login attempt.") });
    }
}