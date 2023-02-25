using Domain.Entity;
using ErrorOr;
using MediatR;

namespace Application.Mediators.UserMediator.Add;

public class AddUserHandler : IRequestHandler<AddUserCommand, ErrorOr<User>>
{
    public Task<ErrorOr<User>> Handle(AddUserCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
