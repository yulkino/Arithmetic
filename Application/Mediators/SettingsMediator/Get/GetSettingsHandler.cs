using Domain.Entity;
using ErrorOr;
using MediatR;

namespace Application.Mediators.SettingsMediator.Get;

public class GetSettingsHandler : IRequestHandler<GetSettingsQuery, ErrorOr<Settings>>
{
    public Task<ErrorOr<Settings>> Handle(GetSettingsQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}