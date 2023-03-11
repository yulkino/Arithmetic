using Application.Mediators.SettingsMediator.Edit;
using Application.ServiceContracts.Repositories.Read;
using Application.ServiceContracts.Repositories.Read.GameReadRepositories;
using Application.ServiceContracts.Repositories.Write.ResolvedGameWriteRepositories;
using Application.Validators.ResolvedGameValidators;
using Domain.Entity.GameEntities;
using ErrorOr;
using FluentValidation;
using MediatR;

namespace Application.Mediators.ResolvedGameMediator.Get;

public class GetResolvedGameHandler : IRequestHandler<GetResolvedGameQuery, ErrorOr<ResolvedGame>>
{
    private readonly IResolvedGameReadRepository _resolvedGameReadRepository;
    private readonly IUserReadRepository _userReadRepository;
    private readonly IGameReadRepository _gameReadRepository;
    private readonly IResolvedGameWriteRepository _resolvedGameWriteRepository;

    public GetResolvedGameHandler(IResolvedGameReadRepository resolvedGameReadRepository, IUserReadRepository userReadRepository,
        IGameReadRepository gameReadRepository, IResolvedGameWriteRepository resolvedGameWriteRepository)
    {
        _resolvedGameReadRepository = resolvedGameReadRepository;
        _userReadRepository = userReadRepository;
        _gameReadRepository = gameReadRepository;
        _resolvedGameWriteRepository = resolvedGameWriteRepository;
    }

    public async Task<ErrorOr<ResolvedGame>> Handle(GetResolvedGameQuery request, CancellationToken cancellationToken)
    {
        var (userId, gameId) = request;

        if (await _userReadRepository.GetUserByIdAsync(userId, cancellationToken) is null)
            return Error.NotFound("General.NotFound", "User does not exist.");

        var game = await _gameReadRepository.GetGameByIdAsync(gameId, userId, cancellationToken);
        if (game is null)
            return Error.NotFound("General.NotFound", "Game does not exist.");

        var resolvedGame = await _resolvedGameReadRepository.GetResolvedGameAsync(userId, gameId, cancellationToken);
        resolvedGame.ProcessGameResult();
        return await _resolvedGameWriteRepository.UpdateResolvedGameAsync(resolvedGame, cancellationToken);
    }
}