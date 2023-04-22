using Application.ServiceContracts.Repositories.Read.SettingsReadRepositories;
using Domain.Entity.SettingsEntities;
using ErrorOr;
using MediatR;

namespace Application.Mediators.DifficultyMediator;

public class GetDifficultiesHandler : IRequestHandler<GetDifficultiesQuery, ErrorOr<List<Difficulty>>>
{
    private readonly IDifficultiesReadRepository _difficultiesReadRepository;

    public GetDifficultiesHandler(IDifficultiesReadRepository difficultiesReadRepository) =>
        _difficultiesReadRepository = difficultiesReadRepository;

    public async Task<ErrorOr<List<Difficulty>>> Handle(GetDifficultiesQuery request,
        CancellationToken cancellationToken)
    {
        return await _difficultiesReadRepository.GetDifficultiesAsync(cancellationToken);
    }
}