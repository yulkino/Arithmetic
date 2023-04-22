using Application.ServiceContracts;
using Application.ServiceContracts.Repositories.Read;
using Application.ServiceContracts.Repositories.Write;
using Domain.Entity;
using ErrorOr;
using MediatR;

namespace Application.Mediators.UserMediator.Add;

public class AddUserHandler : IRequestHandler<AddUserCommand, ErrorOr<User>>
{
    private readonly IUserReadRepository _userReadRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserWriteRepository _userWriteRepository;

    public AddUserHandler(IUserWriteRepository userWriteRepository, IUserReadRepository userReadRepository,
        IUnitOfWork unitOfWork)
    {
        _userWriteRepository = userWriteRepository;
        _userReadRepository = userReadRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<User>> Handle(AddUserCommand request, CancellationToken cancellationToken)
    {
        var (login, password, passwordConfirmation) = request;

        if (await _userReadRepository.GetUserByLoginAsync(login, cancellationToken) is not null)
        {
            return Error.Conflict("User.Conflict", $"User with Login {login} already exists.");
        }

        var user = await _userWriteRepository.AddUserAsync(login, password, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return user;
    }
}