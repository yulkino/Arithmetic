using Application.ClientErrors.Errors;
using Application.ServiceContracts.Repositories.Read;
using Application.ServiceContracts.Repositories.Read.SettingsReadRepositories;
using Domain.Entity.SettingsEntities;
using ErrorOr;
using MediatR;

namespace Application.Mediators.SettingsMediator.Get;

public class GetSettingsHandler : IRequestHandler<GetSettingsQuery, ErrorOr<Settings>>
{
    private readonly ISettingsReadRepository _settingsReadRepository;
    private readonly IUserReadRepository _userReadRepository;
    private readonly IGameReadRepository _gameReadRepository;

    public GetSettingsHandler(ISettingsReadRepository settingsReadRepository, IUserReadRepository userReadRepository,
        IGameReadRepository gameReadRepository)
    {
        _settingsReadRepository = settingsReadRepository;
        _userReadRepository = userReadRepository;
        _gameReadRepository = gameReadRepository;
    }

    public async Task<ErrorOr<Settings>> Handle(GetSettingsQuery request, CancellationToken cancellationToken)
    {
        var (userId, gameId) = request;

        var user = await _userReadRepository.GetUserByIdAsync(userId, cancellationToken);
        if (user is null)
            return Errors.UserErrors.NotFound;

        var game = await _gameReadRepository.GetGameByIdAsync(gameId, cancellationToken);
        if (game is null)
            return Errors.GameErrors.NotFound;

        var settings = await _settingsReadRepository.GetSettingsAsync(game, cancellationToken);
        if (settings is null)
            return Errors.SettingsErrors.NotFound;

        return settings;
    }
}