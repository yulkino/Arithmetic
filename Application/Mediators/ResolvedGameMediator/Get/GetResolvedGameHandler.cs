using Application.Mediators.SettingsMediator.Edit;
using Application.Validators.ResolvedGameValidators;
using Domain.Entity.GameEntities;
using ErrorOr;
using FluentValidation;
using MediatR;

namespace Application.Mediators.ResolvedGameMediator.Get;

public class GetResolvedGameHandler : IRequestHandler<GetResolvedGameQuery, ErrorOr<ResolvedGame>>
{
    private readonly IValidator<GetResolvedGameQuery> _validator;

    public GetResolvedGameHandler(IValidator<GetResolvedGameQuery> validator)
    {
        _validator = validator;
    }

    public Task<ErrorOr<ResolvedGame>> Handle(GetResolvedGameQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}