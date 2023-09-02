using Application.ClientErrors.Errors;
using Application.ServiceContracts;
using Application.ServiceContracts.Repositories.Read;
using Domain.Entity.ExerciseEntities;
using ErrorOr;
using MediatR;

namespace Application.Mediators.GameMediator.SaveExercise;

public class SaveExerciseHandler : IRequestHandler<SaveExerciseCommand, ErrorOr<ResolvedExercise>>
{
    private readonly IExerciseReadRepository _exerciseReadRepository;
    private readonly IGameReadRepository _gameReadRepository;
    private readonly IResolvedGameReadRepository _resolvedGameReadRepository;
    private readonly ITimeProvider _timeProvider;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserReadRepository _userReadRepository;

    public SaveExerciseHandler(IExerciseReadRepository exerciseReadRepository,
        IUserReadRepository userReadRepository, IGameReadRepository gameReadRepository,
        IResolvedGameReadRepository resolvedGameReadRepository, ITimeProvider timeProvider,
        IUnitOfWork unitOfWork)
    {
        _exerciseReadRepository = exerciseReadRepository;
        _userReadRepository = userReadRepository;
        _gameReadRepository = gameReadRepository;
        _resolvedGameReadRepository = resolvedGameReadRepository;
        _timeProvider = timeProvider;
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<ResolvedExercise>> Handle(SaveExerciseCommand request,
        CancellationToken cancellationToken)
    {
        var (userId, gameId, exerciseId, answer) = request;

        var user = await _userReadRepository.GetUserByIdAsync(userId, cancellationToken);
        if (user is null)
            return Errors.UserErrors.NotFound;

        var game = await _gameReadRepository.GetGameByIdAsync(gameId, cancellationToken);
        if (game is null)
            return Errors.GameErrors.NotFound;

        var exercise = await _exerciseReadRepository.GetExerciseByIdAsync(game, exerciseId, cancellationToken);
        if (exercise is null)
            return Errors.ExerciseErrors.NotFound;

        var resolvedGame = await _resolvedGameReadRepository.GetResolvedGameAsync(game, cancellationToken);
        if(resolvedGame is null)
            return Errors.ResolvedGameErrors.NotFound;

        if (resolvedGame.ResolvedExercises.Any(r => r.Exercise.Id == exerciseId))
            return Errors.ResolvedExerciseErrors.ExerciseAlreadyResolved;

        var resolvedExercise = exercise.Resolve(answer, _timeProvider.Now);
        resolvedGame.ResolvedExercises.Add(resolvedExercise);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return resolvedExercise;
    }
}