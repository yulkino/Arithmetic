using Application.ClientErrors.Errors;
using Application.ServiceContracts;
using Application.ServiceContracts.Repositories.Read;
using Application.ServiceContracts.Repositories.Write;
using Application.Services.Authentication;
using Domain.Entity;
using ErrorOr;
using MediatR;

namespace Application.Mediators.UserMediator.Add;

public class AddUserHandler : IRequestHandler<AddUserCommand, ErrorOr<User>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserReadRepository _userReadRepository;
    private readonly IAuthenticationService _authenticationService;
    private readonly IUserWriteRepository _userWriteRepository;

    public AddUserHandler(IUserWriteRepository userWriteRepository, IUserReadRepository userReadRepository,
        IAuthenticationService authenticationService, IUnitOfWork unitOfWork)
    {
        _userWriteRepository = userWriteRepository;
        _userReadRepository = userReadRepository;
        _authenticationService = authenticationService;
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<User>> Handle(AddUserCommand request, CancellationToken cancellationToken)
    {
        var (email, password, passwordConfirmation) = request;

        if (await _userReadRepository.GetUserByLoginAsync(email, cancellationToken) is not null)
            return Errors.UserErrors.Conflict(email);

        if(await _authenticationService.UserExists(email, cancellationToken))
            return Errors.UserErrors.Conflict(email);
        
        var identityId = await _authenticationService.RegisterAsync(email, password, cancellationToken);
        var user = await _userWriteRepository.AddUserAsync(email, identityId, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return user;
    }
}