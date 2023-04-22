using Application.ServiceContracts;
using Application.ServiceContracts.Repositories.Read;
using Domain.Entity.GameEntities;
using ErrorOr;
using MediatR;

namespace Application.Mediators.ResolvedGameMediator.Get;

public class GetResolvedGameHandler : IRequestHandler<GetResolvedGameQuery, ErrorOr<ResolvedGame>>
{
    private readonly IGameReadRepository _gameReadRepository;
    private readonly IResolvedGameReadRepository _resolvedGameReadRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserReadRepository _userReadRepository;

    public GetResolvedGameHandler(IResolvedGameReadRepository resolvedGameReadRepository,
        IUserReadRepository userReadRepository, IGameReadRepository gameReadRepository,
        IUnitOfWork unitOfWork)
    {
        _resolvedGameReadRepository = resolvedGameReadRepository;
        _userReadRepository = userReadRepository;
        _gameReadRepository = gameReadRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<ResolvedGame>> Handle(GetResolvedGameQuery request, CancellationToken cancellationToken)
    {
        var (userId, gameId) = request;

        if (await _userReadRepository.GetUserByIdAsync(userId, cancellationToken) is null)
        {
            return Error.NotFound("User.NotFound", "User does not exist.");
        }

        var game = await _gameReadRepository.GetGameByIdAsync(gameId, userId, cancellationToken);
        if (game is null)
        {
            return Error.NotFound("Game.NotFound", "Game does not exist.");
        }

        var resolvedGame = await _resolvedGameReadRepository.GetResolvedGameAsync(userId, gameId, cancellationToken);
        resolvedGame.ProcessGameResult();

        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return resolvedGame;
    }
}