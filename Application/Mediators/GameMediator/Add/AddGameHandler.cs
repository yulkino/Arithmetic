using Application.Validators.GameValidators;
using Domain.Entity.GameEntities;
using ErrorOr;
using FluentValidation;
using MediatR;

namespace Application.Mediators.GameMediator.Add;

public class AddGameHandler : IRequestHandler<AddGameCommand, ErrorOr<Game>>
{
    private readonly IValidator<AddGameCommand> _validator;

    public AddGameHandler(IValidator<AddGameCommand> validator)
    {
        _validator = validator;
    }

    public Task<ErrorOr<Game>> Handle(AddGameCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}