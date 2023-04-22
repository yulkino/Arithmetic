using Application.ServiceContracts;
using Application.ServiceContracts.Repositories.Read;
using Domain.Entity.ExerciseEntities;
using ErrorOr;
using MediatR;

namespace Application.Mediators.GameMediator.GetExercise;

public class GetExerciseHandler : IRequestHandler<GetExerciseQuery, ErrorOr<Exercise>>
{
    private readonly IGameReadRepository _gameReadRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserReadRepository _userReadRepository;

    public GetExerciseHandler(IGameReadRepository gameReadRepository, IUserReadRepository userReadRepository,
        IUnitOfWork unitOfWork)
    {
        _gameReadRepository = gameReadRepository;
        _userReadRepository = userReadRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<Exercise>> Handle(GetExerciseQuery request, CancellationToken cancellationToken)
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

        var nextExercise = game.GiveNextExercise();
        game.Exercises.Add(nextExercise);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return nextExercise;
    }
}