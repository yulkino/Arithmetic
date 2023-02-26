using Application.Validators.StatisticValidators;
using Domain.Entity;
using ErrorOr;
using FluentValidation;
using MediatR;

namespace Application.Mediators.StatisticMediator.Get;

public class GetStatisticHandler : IRequestHandler<GetStatisticQuery, ErrorOr<Statistic>>
{
    private readonly IValidator<GetStatisticQuery> _validator;

    public GetStatisticHandler(IValidator<GetStatisticQuery> validator)
    {
        _validator = validator;
    }

    public Task<ErrorOr<Statistic>> Handle(GetStatisticQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}