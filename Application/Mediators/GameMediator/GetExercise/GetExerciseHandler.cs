using Application.ServiceContracts.Repositories.Read;
using Application.ServiceContracts.Repositories.Read.GameReadRepositories;
using Application.ServiceContracts.Repositories.Write.GameWriteRepositories;
using Domain.Entity.ExerciseEntities;
using ErrorOr;
using MediatR;

namespace Application.Mediators.GameMediator.GetExercise;

public class GetExerciseHandler : IRequestHandler<GetExerciseQuery, ErrorOr<Exercise>>
{
    private readonly IGameReadRepository _gameReadRepository;
    private readonly IUserReadRepository _userReadRepository;
    private readonly IExerciseWriteRepository _exerciseWriteRepository;

    public GetExerciseHandler(IGameReadRepository gameReadRepository, IUserReadRepository userReadRepository, IExerciseWriteRepository exerciseWriteRepository)
    {
        _gameReadRepository = gameReadRepository;
        _userReadRepository = userReadRepository;
        _exerciseWriteRepository = exerciseWriteRepository;
    }

    public async Task<ErrorOr<Exercise>> Handle(GetExerciseQuery request, CancellationToken cancellationToken)
    {
        var (userId, gameId) = request;

        if (await _userReadRepository.GetUserByIdAsync(userId, cancellationToken) is null)
            return Error.NotFound("User.NotFound", "User does not exist.");

        var game = await _gameReadRepository.GetGameByIdAsync(gameId, userId, cancellationToken);
        if (game is null)
            return Error.NotFound("Game.NotFound", "Game does not exist.");

        var nextExercise = game.GiveNextExercise();
        return await _exerciseWriteRepository.SaveNextExerciseAsync(userId, gameId, nextExercise, cancellationToken);
    }
}