using Application.ServiceContracts;
using Application.ServiceContracts.Repositories.Read;
using Application.ServiceContracts.Repositories.Write;
using Domain.Entity.ExerciseEntities;
using ErrorOr;
using MediatR;

namespace Application.Mediators.GameMediator.SaveExercise;

public class SaveExerciseHandler : IRequestHandler<SaveExerciseCommand, ErrorOr<ResolvedExercise>>
{
    private readonly IExerciseReadRepository _exerciseReadRepository;
    private readonly IGameReadRepository _gameReadRepository;
    private readonly IResolvedGameReadRepository _resolvedGameReadRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserReadRepository _userReadRepository;

    public SaveExerciseHandler(IExerciseReadRepository exerciseReadRepository,
        IUserReadRepository userReadRepository, IGameReadRepository gameReadRepository,
        IResolvedGameReadRepository resolvedGameReadRepository,
        IUnitOfWork unitOfWork)
    {
        _exerciseReadRepository = exerciseReadRepository;
        _userReadRepository = userReadRepository;
        _gameReadRepository = gameReadRepository;
        _resolvedGameReadRepository = resolvedGameReadRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<ResolvedExercise>> Handle(SaveExerciseCommand request,
        CancellationToken cancellationToken)
    {
        var (userId, gameId, exerciseId, answer) = request;

        if (await _userReadRepository.GetUserByIdAsync(userId, cancellationToken) is null)
        {
            return Error.NotFound("User.NotFound", "User does not exist.");
        }

        var game = await _gameReadRepository.GetGameByIdAsync(gameId, userId, cancellationToken);
        if (game is null)
        {
            return Error.NotFound("Game.NotFound", "Game does not exist.");
        }

        var exercise = await _exerciseReadRepository.GetExerciseByIdAsync(exerciseId, cancellationToken);
        if (exercise is null)
        {
            return Error.NotFound("Exercise.NotFound", "Exercise does not exist.");
        }

        var resolvedGame = await _resolvedGameReadRepository.GetResolvedGameAsync(userId, gameId, cancellationToken);

        var resolvedExercise = exercise.Resolve(answer);
        resolvedGame.ResolvedExercises.Add(resolvedExercise);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return resolvedExercise;
    }
}