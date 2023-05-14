using Application.ClientErrors.Errors;
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

        var user = await _userReadRepository.GetUserByIdAsync(userId, cancellationToken);
        if (user is null)
            return Errors.UserErrors.NotFound;

        var game = await _gameReadRepository.GetGameByIdAsync(gameId, cancellationToken);
        if (game is null)
            return Errors.GameErrors.NotFound;

        if (game.Exercises.Count != game.Settings.ExerciseCount)
            return Errors.GameErrors.NotOver;

        var resolvedGame = await _resolvedGameReadRepository.GetResolvedGameAsync(game, cancellationToken);
        if(resolvedGame is null)
            return Errors.ResolvedGameErrors.NotFound;

        resolvedGame.ProcessGameResult();

        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return resolvedGame;
    }
}