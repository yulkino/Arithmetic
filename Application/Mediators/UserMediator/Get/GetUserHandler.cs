using Domain.Entity;
using ErrorOr;
using MediatR;

namespace Application.Mediators.UserMediator.Get;

public class GetUserHandler : IRequestHandler<GetUserQuery, ErrorOr<User>>
{
    public Task<ErrorOr<User>> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}