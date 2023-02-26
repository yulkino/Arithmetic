using Application.Validators.UserValidators;
using Domain.Entity;
using ErrorOr;
using FluentValidation;
using MediatR;

namespace Application.Mediators.UserMediator.Add;

public class AddUserHandler : IRequestHandler<AddUserCommand, ErrorOr<User>>
{
    private readonly IValidator<AddUserCommand> _validator;

    public AddUserHandler(IValidator<AddUserCommand> validator)
    {
        _validator = validator;
    }

    public Task<ErrorOr<User>> Handle(AddUserCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
