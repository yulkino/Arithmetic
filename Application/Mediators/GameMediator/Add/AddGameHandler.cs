using Application.ServiceContracts.Repositories.Read;
using Application.ServiceContracts.Repositories.Write;
using Application.Services.SettingsProvider;
using Domain.Entity.GameEntities;
using ErrorOr;
using MediatR;

namespace Application.Mediators.GameMediator.Add;

public class AddGameHandler : IRequestHandler<AddGameCommand, ErrorOr<Game>>
{
    private readonly IDefaultSettingsProvider _defaultSettingsProvider;
    private readonly IGameWriteRepository _gameWriteRepository;
    private readonly IResolvedGameWriteRepository _resolvedGameWriteRepository;
    private readonly IUserReadRepository _userReadRepository;

    public AddGameHandler(IGameWriteRepository gameWriteRepository, IUserReadRepository userReadRepository,
        IResolvedGameWriteRepository resolvedGameWriteRepository, IDefaultSettingsProvider defaultSettingsProvider)
    {
        _gameWriteRepository = gameWriteRepository;
        _userReadRepository = userReadRepository;
        _resolvedGameWriteRepository = resolvedGameWriteRepository;
        _defaultSettingsProvider = defaultSettingsProvider;
    }

    public async Task<ErrorOr<Game>> Handle(AddGameCommand request, CancellationToken cancellationToken)
    {
        var userId = request.UserId;

        if (await _userReadRepository.GetUserByIdAsync(userId, cancellationToken) is null)
        {
            return Error.NotFound("User.NotFound", "User does not exist.");
        }

        var game = await _gameWriteRepository.CreateAsync(userId, _defaultSettingsProvider.GetDefaultSettings(),
            cancellationToken);
        await _resolvedGameWriteRepository.CreateResolvedGameAsync(game, cancellationToken);
        return game;
    }
}