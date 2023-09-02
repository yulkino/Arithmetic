using Application.ServiceContracts.Repositories.Read.SettingsReadRepositories;
using Domain.Entity.SettingsEntities;
using ErrorOr;
using MediatR;

namespace Application.Mediators.DifficultyMediator.Get;

public class GetDifficultiesHandler : IRequestHandler<GetDifficultiesQuery, ErrorOr<HashSet<Difficulty>>>
{
    private readonly IDifficultiesReadRepository _difficultiesReadRepository;

    public GetDifficultiesHandler(IDifficultiesReadRepository difficultiesReadRepository) =>
        _difficultiesReadRepository = difficultiesReadRepository;

    public async Task<ErrorOr<HashSet<Difficulty>>> Handle(GetDifficultiesQuery request,
        CancellationToken cancellationToken)
    {
        return await _difficultiesReadRepository.GetDifficultiesAsync(cancellationToken);
    }
}