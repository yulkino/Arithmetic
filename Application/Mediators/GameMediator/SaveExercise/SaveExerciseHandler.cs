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
    private readonly IResolvedGameWriteRepository _resolvedGameWriteRepository;
    private readonly IUserReadRepository _userReadRepository;

    public SaveExerciseHandler(IExerciseReadRepository exerciseReadRepository,
        IExerciseWriteRepository exerciseWriteRepository,
        IUserReadRepository userReadRepository, IGameReadRepository gameReadRepository,
        IResolvedGameWriteRepository resolvedGameWriteRepository,
        IResolvedGameReadRepository resolvedGameReadRepository)
    {
        _exerciseReadRepository = exerciseReadRepository;
        _userReadRepository = userReadRepository;
        _gameReadRepository = gameReadRepository;
        _resolvedGameWriteRepository = resolvedGameWriteRepository;
        _resolvedGameReadRepository = resolvedGameReadRepository;
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

        await _resolvedGameWriteRepository.UpdateResolvedGameAsync(resolvedGame, cancellationToken);

        return resolvedExercise;
    }
}