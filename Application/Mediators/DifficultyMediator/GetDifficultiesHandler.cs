using Domain.Entity.SettingsEntities;
using ErrorOr;
using MediatR;

namespace Application.Mediators.DifficultyMediator;

public class GetDifficultiesHandler : IRequestHandler<GetDifficultiesQuery, ErrorOr<List<Difficulty>>>
{
    public Task<ErrorOr<List<Difficulty>>> Handle(GetDifficultiesQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}