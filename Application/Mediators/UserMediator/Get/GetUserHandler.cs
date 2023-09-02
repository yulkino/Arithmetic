using Application.ClientErrors.Errors;
using Application.ServiceContracts.Repositories.Read;
using Application.Services.Authentication;
using ErrorOr;
using MediatR;

namespace Application.Mediators.UserMediator.Get;

public class GetUserHandler : IRequestHandler<GetUserQuery, ErrorOr<GetUserResponse>>
{
    private readonly IUserReadRepository _userReadRepository;
    private readonly IJwtProvider _jwtProvider;

    public GetUserHandler(IUserReadRepository userReadRepository, IJwtProvider jwtProvider)
    {
        _userReadRepository = userReadRepository;
        _jwtProvider = jwtProvider;
    }

    public async Task<ErrorOr<GetUserResponse>> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        var (email, password) = request;

        if (await _userReadRepository.GetUserByLoginAsync(email, cancellationToken) is null)
            return Errors.UserErrors.NotFound;

        var user = await _userReadRepository.LoginUserAsync(email, password, cancellationToken);
        if (user is null)
            return Errors.UserErrors.Failure;

        var response = await _jwtProvider.GetForCredentialsAsync(email, password);
        if (response.IsError)
            return response.FirstError;
        return new GetUserResponse(user.Id, user.Email, response.Value);
    }
}