using Application.Validators.UserValidators;
using Domain.Entity;
using ErrorOr;
using FluentValidation;
using MediatR;

namespace Application.Mediators.UserMediator.Get;

public class GetUserHandler : IRequestHandler<GetUserQuery, ErrorOr<User>>
{
    private readonly IValidator<GetUserQuery> _validator;

    public GetUserHandler(IValidator<GetUserQuery> validator)
    {
        _validator = validator;
    }

    public Task<ErrorOr<User>> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}