using Domain.Entity.Games;
using ErrorOr;
using MediatR;

namespace Application.Mediators.ResolvedGameMediator.Get;

public class GetResolvedGameHandler : IRequestHandler<GetResolvedGameQuery, ErrorOr<ResolvedGame>>
{
    public Task<ErrorOr<ResolvedGame>> Handle(GetResolvedGameQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}