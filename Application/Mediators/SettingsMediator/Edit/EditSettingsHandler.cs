using Application.Validators.SettingsValidators;
using Domain.Entity.SettingsEntities;
using ErrorOr;
using FluentValidation;
using MediatR;

namespace Application.Mediators.SettingsMediator.Edit;

public class EditSettingsHandler : IRequestHandler<EditSettingsCommand, ErrorOr<Settings>>
{
    private readonly IValidator<EditSettingsCommand> _validator;

    public EditSettingsHandler(IValidator<EditSettingsCommand> validator)
    {
        _validator = validator;
    }

    public Task<ErrorOr<Settings>> Handle(EditSettingsCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}