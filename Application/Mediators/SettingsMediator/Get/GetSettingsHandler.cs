using Application.Validators.SettingsValidators;
using Domain.Entity.SettingsEntities;
using ErrorOr;
using FluentValidation;
using MediatR;

namespace Application.Mediators.SettingsMediator.Get;

public class GetSettingsHandler : IRequestHandler<GetSettingsQuery, ErrorOr<Settings>>
{
    private readonly IValidator<GetSettingsQuery> _validator;

    public GetSettingsHandler(IValidator<GetSettingsQuery> validator)
    {
        _validator = validator;
    }

    public Task<ErrorOr<Settings>> Handle(GetSettingsQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}