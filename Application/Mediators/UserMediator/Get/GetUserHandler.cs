using Application.ClientErrors.Errors;
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
        var (login, password) = request;

        if (await _userReadRepository.GetUserByLoginAsync(login, cancellationToken) is null)
            return Errors.UserErrors.NotFound;

        var user = await _userReadRepository.LoginUserAsync(login, password, cancellationToken);
        if (user is null)
            return Errors.UserErrors.Failure;

        return user;
    }
}